using System.Runtime.Serialization;

namespace Firebase
{
    [DataContract]
    public class FireBaseMessage
    { 
        [DataMember(Name = "data")]
        public object Data { get; set; }

        [DataMember(Name="to")]
        public string To { get; set; }

        [DataMember(Name = "notification")]
        public FirebaseNotification Notification       { get; set; }


    }
}
