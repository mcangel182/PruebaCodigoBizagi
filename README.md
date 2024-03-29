PruebaCodigoBizagi
==================

Usted es contratado como ingeniero de desarrollo para Bizagi Process Modeler, la herramientavde modelamiento de procesos de Bizagi. Revisando en los foros de sugerencias de los usuarios, usted encuentra que los usuarios solicitan muy frecuentemente que Bizagi Process Modeler ofrezca una característica de validación de los diagramas de procesos. Usted, con una actitud proactiva, decide complacer a los leales usuarios de Bizagi Process Modeler y diseñar e implementar para ellos esta deseada característica. 
Usted inicia una investigación para determinar qué clase de validaciones necesitarían los usuarios de Bizagi Process Modeler y encuentra este documento: The Rules of BPMN, que contiene un listado de validaciones BPMN que debería cumplir un diagrama de procesos. La aplicación diseñada se ecargará de validar las siguientes reglas Style 0115, BPMN 0102, Style 0104, Style 0122 y Style 0123.

##Objetivos

###1. Funcionalidad mínima esperada por los usuarios
El objetivo principal, es que un usuario mientras está trabajando con un diagrama de procesos pueda validarlo en cualquier momento, la aplicación debe informarle al usuario qué elementos presentan errores y también debe ofrecerle una forma para ir a cualquiera de estos elementos para poder arreglar el error indicado. Dentro del análisis realizado, usted encuentra que un diagrama de procesos puede tener cero, uno, o muchos errores de validación.

###2. Experiencia de usuario
Usted empieza a usar Bizagi Process Modeler, y nota que la experiencia de usuario es uno de losfactores de éxito más importante de la herramienta, por lo tanto usted se traza como objetivo que esta característica mantenga al menos el nivel de usabilidad ofrecido por la herramienta.

###3. Evolución a futuro
Como usted es un ingeniero que siempre piensa en los cambios que pueden venir más adelante, sele ocurre que en el futuro, sería interesante que en el website bizagi.com estuviera disponible este mismo servicio de validación de diagramas de procesos, de tal forma que usuarios que usen otras herramientas de modelamiento de proceso pueden usar este servicio y así atraerlos a usar Bizagi Process Modeler.

##Instrucciones
1. Abrir el archivo PruebaCodigoBizagi.sln en Visual Studio 2012
2. En el Solution Explorer haga click sobre la raiz del proyecto PruebaCodigoBizagi
3. Haciendo click en el boton de correr, corra la aplicación en Google Chrome
4. Seleccione un archivo .xpdl
5. Haga click en validar diagrama. Se mostrará una pantalla con la validación del diagrama seleccionado
6. Para validar otro diagrama haga click en volver y repita los pasos 4 y 5. 
