namespace DataModels.Dtos
{
    public class MessageDto
    {
        /// <summary>
        /// The Customer's Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The Message's Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The Message's Body
        /// </summary>
        public string Body { get; set; }
    }
}
