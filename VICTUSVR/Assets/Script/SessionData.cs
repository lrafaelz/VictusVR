// this object should contain all the session data that is going to be saved in the database
// the array variables are going to register the data of the sensors at each X seconds of the session

[System.Serializable] public class SessionData{
    public int date;
    public string pacientName;
    public float distanceTravelled;
    public int sessionTime;
    public int score;
    public int[] velocity;
    public int[] BPMSensor;
    public int[] EMGSensor;
}