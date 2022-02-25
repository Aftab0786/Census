using IndianCensus.DTO;
using IndianCensus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace IndianCensusTestProject
{
    [TestClass]
    public class UnitTest1
    {

        string stateCensusPath = @"D:\Bridgelabz102\Census\IndianCensus\CSVFiles\IndianPopulation.csv";
        string wrongFilePath = @"D:\Bridgelabz102\Census\IndianCensus\CSVFiles\WrongIndia.csv";
        string wrongTypeFilePath = @"D:\Bridgelabz102\Census\IndianCensus\CSVFiles\IndianPopulation.txt";
        string wrongDelimiterFilePath = @"D:\Bridgelabz102\Census\IndianCensus\CSVFiles\DelimiterIndiaStateCensusData.csv";
        string wrongHeaderFilePath = @"D:\Bridgelabz102\Census\IndianCensus\CSVFiles\WrongIndiaStateCensusData.csv";

        CSVAdapterFactory csv;
        private CSVAdapterFactory csvAdapter;
        Dictionary<string, CensusDTO> stateRecords;

        [TestInitialize]
        public void Setup()
        {
            csv = new CSVAdapterFactory();
            stateRecords = new Dictionary<string, CensusDTO>();
        }
        //TC 1.1
        [TestMethod]
        public void GivenStateCensusCSVShouldReturnRecords()
        {
            stateRecords = csv.LoadCsvData(CensusAnalyser.Country.INDIA, stateCensusPath, "State,Population,AreaInSqKm,DensityPerSqKm");
            Assert.AreEqual(29, stateRecords.Count);
        }

        //TC 1.2
        [TestMethod]
        public void GivenWrongFilePathReturnCustomException()
        {
            string expected = "File Not Found";
            var ex = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(expected, ex.Message);
            Console.WriteLine(ex.Message);
        }

        //TC 1.3
        [TestMethod]
        public void GivenWrongFileTypeThrowCustomException()
        {
            var customException = Assert.ThrowsException<CensusAnalyserException>(() => csvAdapter.LoadCsvData(CensusAnalyser.Country.INDIA, wrongTypeFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, customException.exception);
        }
        // Test case for returning the incorrect delimeter in data custom exception if path is correct(UC1-TC1.4)
        [TestMethod]
        public void GivenWrongDelimiterThrowCustomException()
        {
            var customException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongDelimiterFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, customException.exception);
        }
        //Test case for returning the incorrect header custom exception if path is correct(UC1-TC1.5)
        [TestMethod]
        public void GivenWrongeHeaderThrowCustomException()
        {
            var customException = Assert.ThrowsException<CensusAnalyserException>(() => csv.LoadCsvData(CensusAnalyser.Country.INDIA, wrongHeaderFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, customException.exception);
        }

    }
}
