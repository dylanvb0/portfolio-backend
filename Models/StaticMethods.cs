using System;
using System.Linq;
﻿using System.Security.Cryptography;

namespace portfolio_backend.Models {
  public static class StaticMethods {
    public static string Base64Encode(string plainText){
      var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
      return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData) {
      var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
      return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string SecureRandomString(){
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        var byteArray = new byte[45];
        provider.GetBytes(byteArray);
        return Base64Encode(BitConverter.ToString(byteArray));
    }
  }
}
