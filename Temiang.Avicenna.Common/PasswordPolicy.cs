using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common
{
    public class PasswordPolicy
    {
        public static PasswordValidationResult IsValid(string password)
        {
            int minimum = AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordMinimumLength).ToInt();
            int upperCase = AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordLowerCaseLength).ToInt();
            int lowerCase = AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordUpperCaseLength).ToInt();
            int nonAlpha = AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNonAlphaLength).ToInt();
            int numeric = AppParameter.GetParameterValue(AppParameter.ParameterItem.PasswordNumericLength).ToInt();

            if (minimum > 0 && password.Length < minimum) return PasswordValidationResult.LessThanMinimum;
            if (upperCase > 0 && UpperCaseCount(password) < upperCase) return PasswordValidationResult.UpperCaseNotFound;
            if (lowerCase > 0 && LowerCaseCount(password) < lowerCase) return PasswordValidationResult.LowerCaseNotFound;
            if (numeric > 0 && NumericCount(password) < numeric) return PasswordValidationResult.NumericNotFound;
            if (nonAlpha > 0 && NonAlphaCount(password) < nonAlpha) return PasswordValidationResult.NonAlphaNotFound;
            return PasswordValidationResult.NotValidated;
        }

        private static int UpperCaseCount(string password)
        {
            return Regex.Matches(password, "[A-Z]").Count;
        }

        private static int LowerCaseCount(string password)
        {
            return Regex.Matches(password, "[a-z]").Count;
        }

        private static int NumericCount(string password)
        {
            return Regex.Matches(password, "[0-9]").Count;
        }

        private static int NonAlphaCount(string password)
        {
            return Regex.Matches(password, @"[^0-9a-zA-Z\._]").Count;
        }
    }

    public enum PasswordValidationResult
    {
        NotValidated = -1,
        LessThanMinimum = 0,
        UpperCaseNotFound = 1,
        LowerCaseNotFound = 2,
        NumericNotFound = 3,
        NonAlphaNotFound = 4
    }

    public enum PasswordScore
    {
        Blank = 0,
        VeryWeak = 1,
        Weak = 2,
        Medium = 3,
        Strong = 4,
        VeryStrong = 5
    }

    public class PasswordAdvisor
    {
        public static PasswordScore CheckStrength(string password)
        {
            int score = 0;

            if (password.Length < 1) return PasswordScore.Blank;
            if (password.Length < 4) return PasswordScore.VeryWeak;

            if (password.Length >= 8) score++;
            if (password.Length >= 12) score++;
            if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success) score++;
            if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success && Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success) score++;
            if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success) score++;

            return (PasswordScore)score;
        }
    }
}
