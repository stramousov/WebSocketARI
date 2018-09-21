using System;
using System.Runtime.InteropServices;
using V8.AddIn;

[ComVisible(true)]
[Guid("AF7B87EB-39ED-42B7-85B4-26DDB20DBE85")] // произвольный Guid-идентификатор Вашей компоненты
[ProgId("AddIn.AriNet")] // это имя COM-объекта, по которому Вы будете ее подключать
public class AriNet : LanguageExtenderAddIn
{
    public AriNet() : base(typeof(WebSocketARI.Program), 1000) { }
}