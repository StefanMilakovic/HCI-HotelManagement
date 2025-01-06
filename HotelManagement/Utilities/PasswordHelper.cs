﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace HotelManagement.Utilities
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            // Kreiraj SHA256 hash algoritam
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Pretvori lozinku u byte[] i izračunaj hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Pretvori byte[] u string koristeći Hexadecimal format
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
