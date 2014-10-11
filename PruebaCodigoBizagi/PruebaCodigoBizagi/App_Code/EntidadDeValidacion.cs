using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Collections;
using System.Web.Script.Serialization;

namespace PruebaCodigoBizagi.App_Code
{
    public class EntidadDeValidacion
    {
        public string archivo;
        public XmlDocument doc;
        public XmlNamespaceManager nsmgr;

        public EntidadDeValidacion(string archivo)
        {
            this.archivo = archivo;
            doc = new XmlDocument();
            doc.Load(archivo);
            nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bpm", "http://www.wfmc.org/2008/XPDL2.1");
        }

        public string validarReglasBPMN(String archivo)
        {
            XmlNodeList activities = doc.SelectNodes("//bpm:Activity", nsmgr);
            XmlNodeList transitions = doc.SelectNodes("//bpm:Transition", nsmgr);
            XmlNodeList processes = doc.SelectNodes("//bpm:WorkflowProcess", nsmgr);
            XmlNodeList messageFlows = doc.SelectNodes("//bpm:MessageFlow", nsmgr);

            ArrayList validaciones = new ArrayList();
            validaciones.AddRange(validarBPMN0105(activities, nsmgr));
            validaciones.AddRange(validarBPMN0102(activities, transitions, nsmgr));
            validaciones.AddRange(validarStyle0104(processes, nsmgr));
            validaciones.AddRange(validarStyle0122Y0123(activities, messageFlows, nsmgr));

            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            string json = jsonSerialiser.Serialize(validaciones);

            return json;
        }

        private ArrayList validarBPMN0105(XmlNodeList activities, XmlNamespaceManager nsmgr)
        {
            ArrayList validaciones = new ArrayList();

            foreach (XmlNode activity in activities)
            {
                XmlNode messageEventNode = activity.SelectSingleNode("bpm:Event/bpm:IntermediateEvent", nsmgr);
                if (messageEventNode != null)
                {
                    XmlNode triggerResultMessageNode = messageEventNode.SelectSingleNode("bpm:TriggerResultMessage", nsmgr);
                    XmlAttribute throwEvent = triggerResultMessageNode.Attributes["CatchThrow"];
                    if (throwEvent != null && throwEvent.Value == "THROW")
                    {
                        if (activity.Attributes["Name"] == null)
                        {
                            validaciones.Add(new Validacion("A throwing intermediate event should be labeled.", activity.Attributes["Id"].Value,"(nombre indefinido)", activity));
                        }
                    }
                }
            }
            return validaciones;
        }

        private ArrayList validarBPMN0102(XmlNodeList activities, XmlNodeList transitions, XmlNamespaceManager nsmgr)
        {
            ArrayList validaciones = new ArrayList();
            ArrayList activitiesIds = new ArrayList();
            ArrayList activitiesNodes = new ArrayList();

            foreach (XmlNode activity in activities)
            {
                XmlNodeList endEventNode = activity.SelectNodes("bpm:Event/bpm:EndEvent", nsmgr);
                if (endEventNode.Count == 0)
                {
                    activitiesIds.Add(activity.Attributes["Id"].Value);
                    activitiesNodes.Add(activity);
                }
            }

            foreach (XmlNode transition in transitions)
            {
                int index = activitiesIds.IndexOf(transition.Attributes["From"].Value);
                activitiesIds.Remove(index);
                activitiesNodes.Remove(index);
            }

            for (int i = 0; i < activitiesIds.Count; i++)
            {
                validaciones.Add(new Validacion("All flow objects other than end events and compensating activities must have an outgoing sequence flow, if the process level includes any start or end events.", ((string)activitiesIds[i]), ((XmlNode)activitiesNodes[i]).Attributes["Name"].Value, ((XmlNode)activitiesNodes[i])));
            }
            return validaciones;
        }

        private ArrayList validarStyle0104(XmlNodeList processes, XmlNamespaceManager nsmgr)
        {
            ArrayList validaciones = new ArrayList();

            foreach (XmlNode process in processes)
            {
                XmlNodeList activities = process.SelectNodes("bpm:Activities/bpm:Activity", nsmgr);

                Hashtable hashActividades = new Hashtable();
                foreach (XmlNode activity in activities)
                {
                    if (hashActividades.ContainsKey(activity.Attributes["Name"].Value))
                    {
                        validaciones.Add(new Validacion("Two activities in the same process should not have the same name.", activity.Attributes["Id"].Value, activity.Attributes["Name"].Value, activity));
                    }
                    else
                    {
                        hashActividades.Add(activity.Attributes["Name"].Value, activity);
                    }
                }
            }
            return validaciones;
        }

        private ArrayList validarStyle0122Y0123(XmlNodeList activities, XmlNodeList messageFlows, XmlNamespaceManager nsmgr)
        {
            ArrayList validaciones = new ArrayList();
            ArrayList incomingFlowsIds = new ArrayList();
            ArrayList outgoingFlowsIds = new ArrayList();

            foreach (XmlNode flow in messageFlows)
            {
                outgoingFlowsIds.Add(flow.Attributes["Source"].Value);
                incomingFlowsIds.Add(flow.Attributes["Target"].Value);
            }

            foreach (XmlNode activity in activities)
            {
                XmlNode messageEventNode = activity.SelectSingleNode("bpm:Event/bpm:IntermediateEvent", nsmgr);
                if (messageEventNode != null && messageEventNode.Attributes["Trigger"].Value == "Message")
                {
                    XmlNode triggerResultMessageNode = messageEventNode.SelectSingleNode("bpm:TriggerResultMessage", nsmgr);
                    XmlAttribute throwEvent = triggerResultMessageNode.Attributes["CatchThrow"];
                    if (throwEvent != null && throwEvent.Value == "THROW")
                    {
                        System.Diagnostics.Debug.WriteLine("throw msj");
                        if (!outgoingFlowsIds.Contains(activity.Attributes["Id"].Value))
                        {
                            validaciones.Add(new Validacion("A throwing Message event should have outgoing message flow.", activity.Attributes["Id"].Value, activity.Attributes["Name"].Value, activity));
                        }
                    }
                    else
                    {
                        if (!incomingFlowsIds.Contains(activity.Attributes["Id"].Value))
                        {
                            validaciones.Add(new Validacion("A catching Message event should have incoming message flow.", activity.Attributes["Id"].Value, activity.Attributes["Name"].Value, activity));
                        }
                    }
                }
            }
            return validaciones;
        }
    }
}