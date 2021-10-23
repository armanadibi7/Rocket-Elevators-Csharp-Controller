namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class CallButton
    {
        public int floor;
        public string status;
        public int ID;
        public string direction;
        public CallButton(int _ID,int _floor, string _direction)
        {
            floor = _floor;
            status = "";
             ID = _ID;
             direction = _direction;

        }
    }
}