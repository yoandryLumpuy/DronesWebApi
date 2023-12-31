﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace DronesWebApi.Commons.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException() : base(message: "One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public CustomValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}