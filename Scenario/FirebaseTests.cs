using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace ScenarioTests
{
    public class FirebaseTests
    {
        [Fact]
        public void BasicTests()
        {
            var serverKey = "AIzaSyCt9Xs-W9wNYTB8siG73sPWFJbCRh4le54";
            var aState = new StockState(new List<decimal>() { 20.1m, 10 }, new List<decimal>() { 80.1m, 100 });
            var server = new Firebase.FirebaseServer(serverKey);
            var result = server.SendMessage(new Firebase.FireBaseMessage()
            {
                To = "/topics/all",
                Notification = new Firebase.FirebaseNotification()
                {
                    Title = "Test",
                    Body = "Unit test message"
                },
                Data = aState

            });
            result.ShouldBe(true);
        }
    }
}
