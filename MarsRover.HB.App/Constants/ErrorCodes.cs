namespace MarsRover.HB.App.Constants
{
    public class ErrorCodes
    {
        #region Pletaeu Exceptions

        public const string NullReferenceExceptionForPleteau = "Lütfen Plato için geçerli bir değer giriniz!";
        public const string OutOfRangeExceptionForPleteau = "Lütfen Plato için aralarında boşluk olacak şekilde 2 adet değer giriniz!";
        public const string FormatExceptionForPleteau = "Lütfen Plato için sayısal değerler giriniz";
        public const string NegativeExceptionForPleteau = "Lütfen Plato girilen değerler en az sıfır(0) olabilir!";

        #endregion

        #region Rover Exceptions

        public const string NullReferenceExceptionForRover = "Lütfen Rover için geçerli bir değer giriniz!";
        public const string OutOfRangeExceptionForRover = "Lütfen Rover için aralarında boşluk olacak şekilde 2 adet değer giriniz!";
        public const string FormatExceptionForRover = "Lütfen Rover için sayısal değerler giriniz";
        public const string NegativeExceptionForRover = "Rover girilen değerler en az sıfır(0) olabilir!";
        public const string InvalidDirectionExceptionForRover = "Lütfen Rover için geçerli bir yön değeri giriniz!";
        public const string CrashException = "Hedefe gidilemedi! Kaza uyarısı! Rover güzargahında başka bir Rover ile karşılaştı!";

        #endregion

        #region MapPath Exceptions

        public const string NullReferenceExceptionForMapPath = "Lütfen Hareket Pathi için geçerli bir değer giriniz!";
        public const string InvalidDirectionException = "Hatalı yön değeri girdiniz!";

        #endregion

        #region Movevement Exceptions

        public const string InvalidDirectionExceptionForMapItem = "Lütfen Hareket Pathi için geçerli bir yön değeri giriniz! (L/R/M)";
        public const string OutOfRangeForXCoordinateException = "X koordinatı için sınırı aştınız.";
        public const string OutOfRangeForYCoordinateException = "Y koordinatı için sınırı aştınız.";

        #endregion
    }
}
