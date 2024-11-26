using System.Collections.Generic;
using ekozigPersonEntryDemo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ekozigPersonEntryDemo.Models
{
    public static class EntryValidator
    {
        /// <summary>
        /// Validates an Entry
        /// </summary>
        /// <param name="entry">: entry object</param>
        /// <param name="modelState">: model state which will show error messages if one data type is bad</param>
        public static void ValidateEntry(Entry entry, ModelStateDictionary modelState)
        {
            // Personal Details Validation
            if (string.IsNullOrWhiteSpace(entry.FirstName))
                modelState.AddModelError("FirstName", "A keresztnév megadása kötelező.");

            if (string.IsNullOrWhiteSpace(entry.LastName))
                modelState.AddModelError("LastName", "A vezetéknév megadása kötelező.");

            if (string.IsNullOrWhiteSpace(entry.Email))
                modelState.AddModelError("Email", "Az email megadása kötelező.");
            else if (!entry.Email.Contains("@") || !entry.Email.Contains(".") || entry.Email.IndexOf("@") > entry.Email.LastIndexOf("."))
                modelState.AddModelError("Email", "Az email formátuma rossz.");

            // Address Validation
            if (entry.Address == null)
            {
                modelState.AddModelError("Address", "A cím megadása kötelező.");
                return;
            }

            if (string.IsNullOrWhiteSpace(entry.Address.PostCode))
                modelState.AddModelError("Address.PostCode", "Az irányítószám megadása kötelező.");
            else if (entry.Address.PostCode.Length != 4)
                modelState.AddModelError("Address.PostCode", "Az irányítószámnak pontosan 4 karakterúnek kell lennie.");

            if (string.IsNullOrWhiteSpace(entry.Address.Town))
                modelState.AddModelError("Address.Town", "A település megadása kötelező.");

            if (string.IsNullOrWhiteSpace(entry.Address.Street))
                modelState.AddModelError("Address.Street", "Az utca megadása kötelező.");

            if (string.IsNullOrWhiteSpace(entry.Address.HouseNumber.ToString()))
                modelState.AddModelError("Address.HouseNumber", "Az házszám megadása kötelező.");

            if (entry.Address.HouseNumber <= 0)
                modelState.AddModelError("Address.HouseNumber", "A házszámnak nagyobbnak kell lennie, mint nulla.");
        }
    }

}
