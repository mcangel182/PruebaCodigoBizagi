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
        public string diagrama;

        public EntidadDeValidacion(string archivo)
        {
            this.archivo = archivo;
            doc = new XmlDocument();
            doc.Load(archivo);
            nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bpm", "http://www.wfmc.org/2008/XPDL2.1");
        }

        public string validarReglasBPMN()
        {
            XmlNodeList activities = doc.SelectNodes("//bpm:Activity", nsmgr);
            XmlNodeList transitions = doc.SelectNodes("//bpm:Transition", nsmgr);
            XmlNodeList processes = doc.SelectNodes("//bpm:WorkflowProcess", nsmgr);
            XmlNodeList messageFlows = doc.SelectNodes("//bpm:MessageFlow", nsmgr);

            ArrayList validaciones = new ArrayList();
            diagrama = archivo;
            validaciones.AddRange(validarBPMN0105(activities, nsmgr));
            validaciones.AddRange(validarBPMN0102(activities, transitions, nsmgr));
            validaciones.AddRange(validarStyle0104(processes, nsmgr));
            validaciones.AddRange(validarStyle0122Y0123(activities, messageFlows, nsmgr));
            string json;
            if (validaciones.Count == 0)
            {
                json = "PASO_VALIDACIONES" + diagrama;
            }
            else
            {
                JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
                json = jsonSerialiser.Serialize(validaciones);
            }
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
                        if (activity.Attributes["Name"].Value == "")
                        {
                            Validacion v = new Validacion();
                            v.idElemento = activity.Attributes["Id"].Value;
                            v.nomElemento = activity.Attributes["Name"].Value;
                            v.xpathElement = v.FindXPath(activity);
                            v.numeroDiagrama = diagrama;
                            v.mensaje = "<h5>El elemento " + v.nomElemento + " con id " + v.idElemento + " viola la siguiente validación: </h5>" + "A throwing intermediate event should be labeled.";
                            validaciones.Add(v);
                        }
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("val1 = "+validaciones.Count);
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
                activitiesIds.RemoveAt(index);
                activitiesNodes.RemoveAt(index);
            }

            for (int i = 0; i < activitiesIds.Count; i++)
            {
                Validacion v = new Validacion();
                v.idElemento = ((string)activitiesIds[i]);
                v.nomElemento = ((XmlNode)activitiesNodes[i]).Attributes["Name"].Value;
                v.xpathElement = v.FindXPath(((XmlNode)activitiesNodes[i]));
                v.numeroDiagrama = diagrama;
                v.mensaje = "<h5>El elemento " + v.nomElemento + " con id " + v.idElemento + " viola la siguiente validación: </h5>" + "All flow objects other than end events and compensating activities must have an outgoing sequence flow, if the process level includes any start or end events.";
                validaciones.Add(v);
            }

            System.Diagnostics.Debug.WriteLine("val2 = " + validaciones.Count);
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
                        Validacion v = new Validacion();
                        v.idElemento = activity.Attributes["Id"].Value;
                        v.nomElemento = activity.Attributes["Name"].Value;
                        v.xpathElement = v.FindXPath(activity);
                        v.numeroDiagrama = diagrama;
                        v.mensaje = "<h5>El elemento " + v.nomElemento + " con id " + v.idElemento + " viola la siguiente validación: </h5>" + "Two activities in the same process should not have the same name.";
                        validaciones.Add(v);
                    }
                    else if (activity.Attributes["Name"].Value != "")
                    {
                        hashActividades.Add(activity.Attributes["Name"].Value, activity);
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("val3 = " + validaciones.Count);
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
                        if (!outgoingFlowsIds.Contains(activity.Attributes["Id"].Value))
                        {
                            Validacion v = new Validacion();
                            v.idElemento = activity.Attributes["Id"].Value;
                            v.nomElemento = activity.Attributes["Name"].Value;
                            v.xpathElement = v.FindXPath(activity);
                            v.numeroDiagrama = diagrama;
                            v.mensaje = "<h5>El elemento " + v.nomElemento + " con id " + v.idElemento + " viola la siguiente validación: </h5>" + "A throwing Message event should have outgoing message flow.";
                            validaciones.Add(v);
                        }
                    }
                    else
                    {
                        if (!incomingFlowsIds.Contains(activity.Attributes["Id"].Value))
                        {
                            Validacion v = new Validacion();
                            v.idElemento = activity.Attributes["Id"].Value;
                            v.nomElemento = activity.Attributes["Name"].Value;
                            v.xpathElement = v.FindXPath(activity);
                            v.numeroDiagrama = diagrama;
                            v.mensaje = "<h5>El elemento " + v.nomElemento + " con id " + v.idElemento + " viola la siguiente validación: </h5>" + "A catching Message event should have incoming message flow.";
                            validaciones.Add(v);
                        }
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("val4 = " + validaciones.Count);
            return validaciones;
        }
    }
}