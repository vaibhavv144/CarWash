

    public interface ISMTP
    {
        public Task SendEmailAsync(string to, string subject, string body);
    }
