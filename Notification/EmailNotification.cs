using System.Text;

namespace SD_125_BugTracker.Notification
{
    public class EmailNotification
    {

        static HttpClient Client = new HttpClient();
        static string apiEndpoint = "https://api.courier.com/send";
        static string token = "Bearer " + "pk_prod_5481WC0EBTMG5AJJCYVWZ7M18X61";

        public async Task TicketUpdateEmail(string recipientEmail, string recipientName, string ticketName, string ticketDescription, string projectName, string ticketType, string ticketPriority, string propertyChanged, string oldValue, string newValue)
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Authorization", token);

                string payload = "{ \"message\": { \"routing\": {\"method\": \"single\",\"channels\": []},\"channels\": {},\"providers\": {},\"metadata\": {\"tags\": [],\"utm\": {}     }, \"to\": {\"data\": {\"recipientName\":" + $"\"{recipientName}\"" + ", \"projectName\":" + $"\"{projectName}\"" + ", \"ticketName\":" + $"\"{ticketName}\"" + ", \"ticketDescription\":" + $"\"{ticketDescription}\"" + ", \"ticketType\":" + $"\"{ticketType}\"" + ", \"ticketPriority\":" + $"\"{ticketPriority}\"" + ", \"updateDateTime\":" + $"\"{DateTime.Now.ToString("g")}\"" + ", \"propertyChanged\":" + $"\"{propertyChanged}\"" + ", \"oldValue\":" + $"\"{oldValue}\"" + ", \"newValue\":" + $"\"{newValue}\"" + "},\"preferences\": {},\"email\":" + $"\"{recipientEmail}\"" + "},\"template\": \"S4ARJ5HE4NMC9PPZCWKQ0QP64KAZ\"}}";

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var resp = await Client.PostAsync(new Uri(apiEndpoint), content);
                if ( !resp.IsSuccessStatusCode )
                {
                    throw new Exception(resp.ReasonPhrase);
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        public async Task NewTicketCommentEmail(string recipientEmail, string recipientName, string ticketName, string ticketDescription, string projectName, string ticketType, string ticketPriority, string commenter, string commentBody)
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Authorization", token);

                string payload = "{ \"message\": { \"routing\": {\"method\": \"single\",\"channels\": []},\"channels\": {},\"providers\": {},\"metadata\": {\"tags\": [],\"utm\": {}     }, \"to\": {\"data\": {\"recipientName\":" + $"\"{recipientName}\"" + ", \"projectName\":" + $"\"{projectName}\"" + ", \"ticketName\":" + $"\"{ticketName}\"" + ", \"ticketDescription\":" + $"\"{ticketDescription}\"" + ", \"ticketType\":" + $"\"{ticketType}\"" + ", \"ticketPriority\":" + $"\"{ticketPriority}\"" + ", \"newCommentDate\":" + $"\"{DateTime.Now.ToString("g")}\"" + ", \"commenter\":" + $"\"{commenter}\"" + ", \"commentBody\":" + $"\"{commentBody}\"" + "},\"preferences\": {},\"email\":" + $"\"{recipientEmail}\"" + "},\"template\": \"R3MJFR6KFKM54WN9YHZBG7PMMNEH\"}}";

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var resp = await Client.PostAsync(new Uri(apiEndpoint), content);
                if ( !resp.IsSuccessStatusCode )
                {
                    throw new Exception(resp.ReasonPhrase);
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        public async Task NewTicketAttachmentEmail(string recipientEmail, string recipientName, string ticketName, string ticketDescription, string projectName, string ticketType, string ticketPriority)
        {
            // WORK ON THIS
            try
            {
                Client.DefaultRequestHeaders.Add("Authorization", token);

                string payload = "{ \"message\": { \"routing\": {\"method\": \"single\",\"channels\": []},\"channels\": {},\"providers\": {},\"metadata\": {\"tags\": [],\"utm\": {}     }, \"to\": {\"data\": {\"recipientName\":" + $"\"{recipientName}\"" + ", \"projectName\":" + $"\"{projectName}\"" + ", \"ticketName\":" + $"\"{ticketName}\"" + ", \"ticketDescription\":" + $"\"{ticketDescription}\"" + ", \"ticketType\":" + $"\"{ticketType}\"" + ", \"ticketPriority\":" + $"\"{ticketPriority}\"" + ", \"attachmentDateTime\":" + $"\"{DateTime.Now.ToString("g")}\"" + "},\"preferences\": {},\"email\":" + $"\"{recipientEmail}\"" + "},\"template\": \"\"}}";

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var resp = await Client.PostAsync(new Uri(apiEndpoint), content);
                if ( !resp.IsSuccessStatusCode )
                {
                    throw new Exception(resp.ReasonPhrase);
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        public async Task TicketAssignEmail(string recipientEmail, string recipientName, string ticketName, string ticketDescription, string projectName, string ticketType, string ticketPriority)
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Authorization", token);

                string payload = "{ \"message\": { \"routing\": {\"method\": \"single\",\"channels\": []},\"channels\": {},\"providers\": {},\"metadata\": {\"tags\": [],\"utm\": {}     }, \"to\": {\"data\": {\"recipientName\":" + $"\"{recipientName}\"" + ", \"projectName\":" + $"\"{projectName}\"" + ", \"ticketName\":" + $"\"{ticketName}\"" + ", \"ticketDescription\":" + $"\"{ticketDescription}\"" + ", \"ticketType\":" + $"\"{ticketType}\"" + ", \"ticketPriority\":" + $"\"{ticketPriority}\"" + ", \"createdDateTime\":" + $"\"{DateTime.Now.ToString("g")}\"" + "},\"preferences\": {},\"email\":" + $"\"{recipientEmail}\"" + "},\"template\": \"3CFKR3KYGM4R2KQXRWTA513QKYW4\"}}";

                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var resp = await Client.PostAsync(new Uri(apiEndpoint), content);
                if ( !resp.IsSuccessStatusCode )
                {
                    throw new Exception(resp.ReasonPhrase);
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
    }
}

