using Shared.Utilities.Constants;

namespace Shared.Utilities.Handlers
{
    public static class StringOperations
    {
        /// <summary>
        /// To mask a text
        /// </summary>
        /// <param name="value">The text to be masked.</param>
        /// <param name="charaterMaskingPercentage">The percentage of the text length to be masked.</param>
        /// <returns></returns>
        public static string MaskString(this string value, int charaterMaskingPercentage)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (charaterMaskingPercentage == 0)
                return value;

            string maskedString = string.Empty;
            var characterArray = value.ToCharArray();
            int valueCount = characterArray.Length;

            double lengthToMask = charaterMaskingPercentage / (double)GenericConstants.HIGHEST_PERCENTAGE * valueCount;
            int wholeLengthToMask = Convert.ToInt32(Math.Round(lengthToMask));
            for (int i = 1; i <= valueCount; i++)
            {
                if (i <= wholeLengthToMask)
                {
                    maskedString += "X";
                }
                else
                {
                    maskedString += characterArray[i - 1];
                }
            }

            return maskedString;
        }
    }
}
