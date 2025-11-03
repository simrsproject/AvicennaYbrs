namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceRoom
    {
        public static string GetRoomName(string roomID)
        {
            if (string.IsNullOrWhiteSpace(roomID)) return string.Empty;

            var ent = new ServiceRoom();
            if (ent.LoadByPrimaryKey(roomID))
                return ent.RoomName;

            return string.Empty;
        } 
    }
}
