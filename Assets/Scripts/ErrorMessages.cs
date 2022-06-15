using System.Collections;
using System.Collections.Generic;
using GameSparks.Core;

public static class ErrorMessages
{
    /*  private static readonly Dictionary<string, string> registrationErrors = new Dictionary<string, string>()
       {
           { "TAKEN", "THAT USERNAME IS ALREADY TAKEN. PLEASE CHOOSE ANOTHER."}
       };
      private static readonly Dictionary<string, string> authErrors = new Dictionary<string, string>()
       {
           { "UNRECOGNISED", "EITHER YOUR USERNAME OR PASSWORD IS INCORRECT. CHECK TO MAKE SURE YOU SPELLED THEM CORRECTLY."},
           { "LOCKED", "THERE HAVE BEEN TOO MANY FAILED LOGINS WITH THESE CREDENTIALS, SO THE ACCOUNT HAS BEEN TEMPORARILY LOCKED. PLEASE TRY AGAIN LATER."}
       };
     private static readonly Dictionary<string, string> purchaseErrors = new Dictionary<string, string>()
       {
           { "EXCEEDS_MAX_QUANTITY", "YOU ALREADY HAVE THIS ITEM!"},
           { "DISABLED", "THIS PURCHASE HAS BEEN TEMPORARILY DISABLED. CHECK BACK LATER."}
       };*/
    public static string whatsMyErrorMessage(string typeOfPurchase, GSData errors)
    {
        if (typeOfPurchase.Equals("PURCHASE"))
        {
            if (errors.GetString("quantity") == "EXCEEDS_MAX_QUANTITY")
            {
                return "YOU ALREADY HAVE THIS ITEM!";
            }
            if (errors.GetString("shortCode") == "DISABLED")
            {
                return "THIS PURCHASE HAS BEEN TEMPORARILY DISABLED. CHECK BACK LATER.";
            }
        }
        if (typeOfPurchase.Equals("REGISTRATION"))
        {
            if (errors.GetString("USERNAME") == "TAKEN")
            {
                return "THAT USERNAME IS ALREADY TAKEN. PLEASE CHOOSE ANOTHER.";
            }
        }
        if (typeOfPurchase.Equals("AUTH"))
        {
            if (errors.GetString("DETAILS") == "UNRECOGNISED")
            {
                return "EITHER YOUR USERNAME OR PASSWORD IS INCORRECT. CHECK TO MAKE SURE YOU SPELLED THEM CORRECTLY.";
            }
            if (errors.GetString("DETAILS") == "LOCKED")
            {
                return "THERE HAVE BEEN TOO MANY FAILED LOGINS WITH THESE CREDENTIALS, SO THE ACCOUNT HAS BEEN TEMPORARILY LOCKED. PLEASE TRY AGAIN LATER.";
            }
        }
        return "UNKNOWN ERROR! THE ROBOTS ARE FAILING! GAAAAHHHHHH";
    }
}
