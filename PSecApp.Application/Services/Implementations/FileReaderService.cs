using Excel = Microsoft.Office.Interop.Excel;
using PSecApp.Application.Services.Abstractions;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data;
using PSecApp.Application.DTOs;
using System.Diagnostics.Contracts;
using PSecApp.Domain.Entities;
using PSecApp.Domain.Enums;
using System;
using PSecApp.Domain.Interfaces;
using PSecApp.Application.Models;

namespace PSecApp.Application.Services.Implementations
{
    /// <summary>
    /// Reads Excel file content & nothing else (SRP)    
    /// </summary>
    public class FileReaderService
        : IFileReaderService<DailyMTM, DownloadFile>
    {
        // TODO:  Logger
        private readonly IFileValidatorService _fileValidatorService;
        public FileReaderService(IFileValidatorService fileValidatorService)
        {
            _fileValidatorService = fileValidatorService;
        }

        #region Public Methods

        /// <summary>
        /// Read Excel content using Interop.Excel
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public async Task<List<DailyMTM>> ReadDataFromAFileAsync(DownloadFile source)
        {
            // Do not process empty file (0KB)
            string path = Path.Combine(source.DestinationFolder, source.DestinationFileName);

            if (_fileValidatorService.IsFileEmpty(path)) return new List<DailyMTM>();

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            var mappedData = this.MapWorksheetToEntityList(xlRange, source.FileDate);
            xlWorkbook.Close();
            xlApp.Quit();

            return mappedData;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Converts worksheet into a list of Object
        /// </summary>
        /// <param name="xlRange"></param>
        /// <returns></returns>
        private List<DailyMTM> MapWorksheetToEntityList(Excel.Range xlRange, DateTime fileDate)
        {
            List<DailyMTM> dailyMTMs = new();

            // Convert worksheet range into a 2D array.
            object[,] worksheetAsArray = (object[,])xlRange.Value2;

            // For our files, ingore row 1,2,3 and 5
            for (int xlrow = 6; xlrow < worksheetAsArray.GetUpperBound(0); xlrow++)
            {
                string contract = (string)worksheetAsArray[xlrow, (int)FileEntityMapping.Contract];
                if (string.IsNullOrEmpty(contract))
                {
                    break; // Reached the end of the file data.
                }

                //Map Excel columns to Entity                
                dailyMTMs.Add(new DailyMTM()
                {
                    FileDate = fileDate,
                    Contract = (string)worksheetAsArray[xlrow, (int)FileEntityMapping.Contract], // Map to Column A
                    ExpiryDate = DateTime.FromOADate(Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.ExpiryDate])), // Map to Column C
                    Classification = (string)worksheetAsArray[xlrow, (int)FileEntityMapping.Classification], // Map to Column D

                    Strike = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.Strike]), // Map to Column E "Strike"
                    CallPut = (string)worksheetAsArray[xlrow, (int)FileEntityMapping.CallPut], // Map to Column E "Call/Put"
                    MTMYield = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.MTMYield]), // Map to Column E "MTM Yield"
                    MarkPrice = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.MarkPrice]), // Map to Column E "Mark Price"
                    SpotRate = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.SpotRate]), // Map to Column E "Spot Rate"
                    PreviousMTM = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.PreviousMTM]), // Map to Column E "Previous MTM"
                    PreviousPrice = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.PreviousPrice]), // Map to Column E "Previous Price"
                    PremiumOnOption = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.PremiumOnOption]), // Map to Column E "Premium On Option"

                    //TODO: Fix bug
                    //Volatility = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.Volatility].ToString().Replace(".", ",")), // Map to Column E "Volatility"
                    Delta = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.Delta]), // Map to Column E "Delta" 
                    DeltaValue = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.DeltaValue]), // Map to Column E "Delta Value" 
                    ContractsTraded = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.ContractsTraded]), // Map to Column E "ContractsTraded" 
                    OpenInterest = Convert.ToDouble(worksheetAsArray[xlrow, (int)FileEntityMapping.OpenInterest]) // Map to Column E "Open Interest" 
                });
            }

            return dailyMTMs;
        }

        #endregion      
    }
}
