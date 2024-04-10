namespace Websocket.Model
{
    public enum ActionEnum
    {
        ServerBitHeart,ClintBitHeart, ServerSelect,Vote,ClientSelect
    }

    public class MsgPack
    {
        public ActionEnum action { get; set; }
        public string Msg { get; set; }
    }
    public class PreSelectModel
    {
     
        public ConnectHost Selected { get; set; }

    }
    public class ServerBoardCast
    {
        public List<HostModel> AllInfo { get; set; }
    }
   
    public class ClientBitHeart
    {
        public HostModel ClientInfo { get; set; }

    }

  

}
