using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Domain.Enums
{
    public enum FileEntityMapping
    {
        Contract =  1, // Map to Column A
        ExpiryDate = 3, // Map to Column C
        Classification = 4, // Map to Column D
        Strike = 5, // Map to Column E "Strike"
        CallPut = 6, // Map to Column E "Call/Put"
        MTMYield = 7, // Map to Column E "MTM Yield"
        MarkPrice = 8, // Map to Column E "Mark Price"
        SpotRate = 9, // Map to Column E "Spot Rate"
        PreviousMTM = 10, // Map to Column E "Previous MTM"
        PreviousPrice = 11, // Map to Column E "Previous Price"
        PremiumOnOption = 12, // Map to Column E "Premium On Option"
        Volatility = 13, // Map to Column E "Volatility"
        Delta = 14, // Map to Column E "Delta" 
        DeltaValue = 15, // Map to Column E "Delta Value" 
        ContractsTraded = 16, // Map to Column E "ContractsTraded" 
        OpenInterest = 17, // Map to Column E "Open Interest" 
    }
}
