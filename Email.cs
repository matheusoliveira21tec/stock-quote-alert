using System.Net.Mail;

public class Email
{
    public bool sendMessage(bool venda)
    {
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

        var reader = new StreamReader(File.OpenRead(@"Email.csv"));
        var line = reader.ReadLine();
        var values = line.Split(';');
        client.Host = values[1];
        client.Port = 587;
        client.UseDefaultCredentials = false;
        client.EnableSsl = true;
        client.Credentials = new System.Net.NetworkCredential("testeapiinoa@gmail.com", "senhadeteste");
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("testeapiinoa@gmail.com");
        mail.To.Add(new MailAddress(values[0]));
        mail.Subject = "Alerta de trade!!!";
        if (venda) { mail.Body = "Sr(a), nosso sistema de monitoramento de ativos na B3 detectou uma subida na ação a qual criou um alerta, hora de vender!!"; }
        else { mail.Body = "Sr(a), nosso sistema de monitoramento de ativos na B3 detectou uma descida na ação a qual criou um alerta, hora de comprar!!"; }
        client.Send(mail);
        return true;
    }
}
