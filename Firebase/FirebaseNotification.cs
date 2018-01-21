using System.Runtime.Serialization;

namespace Firebase
{
    [DataContract]
    public class FirebaseNotification
    {
        [DataMember(Name ="title")]
        public string Title { get; set; }
        [DataMember(Name = "body")]
        public string Body { get; set; }
     
    }
}
