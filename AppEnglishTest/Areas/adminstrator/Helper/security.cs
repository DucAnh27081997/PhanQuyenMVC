using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AppEnglishTest.Areas.administrator.Helper
{
    public class security
    {

        public static string RandomStringGenerator(int codeLength)
        {
            string box = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefjhijklmnopqrstuvwxyz0123456789";
            int boxLength = box.Length;
            string code = "";

            Random rd = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                code += box[rd.Next(0, boxLength)];
            }
            return code;
        }

        // ham băm ngẫu nhiên
        public static string HashPass(string pass, string salt)
        {
            //Tạo MD5 
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(salt + pass);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return (sb.ToString());
        }

        // so sanh pass xem dung ko 
        public static Boolean chekPass(string pass, string slat, string code)
        {
            if (HashPass(pass, slat) == code)
            {
                return true;
            }
            return false;
        }
        // gui mail
        public static bool Send_Email(string SendTo, string Subject, string Body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("ducmta270897@gmail.com");
                mail.To.Add(SendTo);
                mail.Subject = Subject;
                mail.Body = Body;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ducmta270897@gmail.com", "anhnhoem97");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public static void Encrypt(string publicKeyFileName, string plainFileName, string encryptedFileName)

        {

            // Variables

            CspParameters cspParams = null;

            RSACryptoServiceProvider rsaProvider = null;

            StreamReader publicKeyFile = null;

            StreamReader plainFile = null;

            FileStream encryptedFile = null;

            string publicKeyText = "";

            string plainText = "";

            byte[] plainBytes = null;

            byte[] encryptedBytes = null;

            try

            {

                // Select target CSP

                cspParams = new CspParameters();

                cspParams.ProviderType = 1; // PROV_RSA_FULL

                //cspParams.ProviderName; // CSP name

                rsaProvider = new RSACryptoServiceProvider(cspParams);

                // Read public key from file

                publicKeyFile = File.OpenText(publicKeyFileName);

                publicKeyText = publicKeyFile.ReadToEnd();

                // Import public key

                rsaProvider.FromXmlString(publicKeyText);

                // Read plain text from file

                plainFile = File.OpenText(plainFileName);

                plainText = plainFile.ReadToEnd();

                // Encrypt plain text

                plainBytes = Encoding.Unicode.GetBytes(plainText);

                encryptedBytes = rsaProvider.Encrypt(plainBytes, false);

                // Write encrypted text to file

                encryptedFile = File.Create(encryptedFileName);

                encryptedFile.Write(encryptedBytes, 0, encryptedBytes.Length);

            }

            catch (Exception ex)

            {

                // Any errors? Show them

                //Console.WriteLine("Exception encrypting file! More info:");

                //Console.WriteLine(ex.Message);

            }

            finally

            {

                // Do some clean up if needed

                if (publicKeyFile != null)

                {

                    publicKeyFile.Close();

                }

                if (plainFile != null)

                {

                    plainFile.Close();

                }

                if (encryptedFile != null)

                {

                    encryptedFile.Close();

                }

            }

        } // Encrypt

        public static void Decrypt(string privateKeyFileName, string encryptedFileName, string plainFileName)

        {

            // Variables

            CspParameters cspParams = null;

            RSACryptoServiceProvider rsaProvider = null;

            StreamReader privateKeyFile = null;

            FileStream encryptedFile = null;

            StreamWriter plainFile = null;

            string privateKeyText = "";

            string plainText = "";

            byte[] encryptedBytes = null;

            byte[] plainBytes = null;

            try{

                // Select target CSP

                cspParams = new CspParameters();

                cspParams.ProviderType = 1; // PROV_RSA_FULL

                //cspParams.ProviderName; // CSP name

                rsaProvider = new RSACryptoServiceProvider(cspParams);

                // Read private/public key pair from file

                privateKeyFile = File.OpenText(privateKeyFileName);

                privateKeyText = privateKeyFile.ReadToEnd();

                // Import private/public key pair

                rsaProvider.FromXmlString(privateKeyText);

                // Read encrypted text from file

                encryptedFile = File.OpenRead(encryptedFileName);

                encryptedBytes = new byte[encryptedFile.Length];

                encryptedFile.Read(encryptedBytes, 0, (int)encryptedFile.Length);

                // Decrypt text

                plainBytes = rsaProvider.Decrypt(encryptedBytes, false);

                // Write decrypted text to file

                plainFile = File.CreateText(plainFileName);

                plainText = Encoding.Unicode.GetString(plainBytes);

                plainFile.Write(plainText);

            }

            catch (Exception ex)

            {

                // Any errors? Show them

                //Console.WriteLine("Exception decrypting file! More info:");

                //Console.WriteLine(ex.Message);

            }

            finally

            {

                // Do some clean up if needed

                if (privateKeyFile != null)

                {

                    privateKeyFile.Close();

                }

                if (encryptedFile != null)

                {

                    encryptedFile.Close();

                }

                if (plainFile != null)

                {

                    plainFile.Close();

                }

            }

        } // Decrypt

    }

}
