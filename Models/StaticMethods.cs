using System;
using System.Linq;
﻿using System.Security.Cryptography;
using portfolio_backend.Controllers;

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

    public static bool ClientAuthorized(string namespce, string authHeader){
      if(authHeader != null && authHeader.StartsWith("Bearer")){
        string token = authHeader.Substring("Bearer ".Length).Trim();
        token = StaticMethods.Base64Decode(token);
        var clients = ClientsController.GetDatastore().List();
        Client client = null;
        foreach(Client c in clients){
          if(namespce == c.Namespace){
            client = c;
          }
        }
        if(client == null) return false;
        if(StaticMethods.Base64Decode(client.SessionToken) == token &&
           client.TokenExpiration.ToUniversalTime() > DateTime.Now.ToUniversalTime()){
              client.TokenExpiration = DateTime.Now.ToUniversalTime().AddMinutes(30);
              ClientsController.GetDatastore().Update(client);
              return true;
        }
        return false;
      }else{
        return false;
      }
    }
  }
}
