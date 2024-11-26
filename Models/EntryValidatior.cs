using System.Collections.Generic;
using ekozigPersonEntryDemo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ekozigPersonEntryDemo.Models
{
    public static class EntryValidator
    {
        public static void ValidateEntry(Entry entry, ModelStateDictionary modelState)
        {
            // Personal Details Validation
            if (string.IsNullOrWhiteSpace(entry.FirstName))
                modelState.AddModelError("FirstName", "First Name is required.");

            if (string.IsNullOrWhiteSpace(entry.LastName))
                modelState.AddModelError("LastName", "Last Name is required.");

            if (string.IsNullOrWhiteSpace(entry.Email))
                modelState.AddModelError("Email", "Email is required.");
            else if (!entry.Email.Contains("@") || !entry.Email.Contains(".") || entry.Email.IndexOf("@") > entry.Email.LastIndexOf("."))
                modelState.AddModelError("Email", "Invalid email format.");

            if (string.IsNullOrWhiteSpace(entry.Phone))
                modelState.AddModelError("Phone", "Phone number is required.");

            // Address Validation
            if (entry.Address == null)
            {
                modelState.AddModelError("Address", "Address is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(entry.Address.PostCode))
                modelState.AddModelError("Address.PostCode", "Post Code is required.");
            else if (entry.Address.PostCode.Length != 4)
                modelState.AddModelError("Address.PostCode", "Post Code must be exactly 4 characters.");

            if (string.IsNullOrWhiteSpace(entry.Address.Town))
                modelState.AddModelError("Address.Town", "Town is required.");

            if (string.IsNullOrWhiteSpace(entry.Address.Street))
                modelState.AddModelError("Address.Street", "Street is required.");

            if (entry.Address.HouseNumber <= 0)
                modelState.AddModelError("Address.HouseNumber", "House Number must be greater than 0.");
        }
    }

}
