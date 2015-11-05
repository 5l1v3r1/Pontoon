﻿//-----------------------------------------------------------------------
// <copyright file="EmailRecipient.cs" company="In The Hand Ltd">
//     Copyright © 2014-15 In The Hand Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Text;

namespace InTheHand.ApplicationModel.Email
{
    /// <summary>
    /// Represents an email recipient.
    /// </summary>
    public sealed class EmailRecipient
    {
        /// <summary>
        /// Initializes an instance of the EmailRecipient class.
        /// </summary>
        public EmailRecipient() { }

        /// <summary>
        /// Initializes an instance of the EmailRecipient class.
        /// </summary>
        /// <param name="address">The address of the recipient.</param>
        public EmailRecipient(string address) 
        { 
            this.Address = address; 
        }

        /// <summary>
        /// Initializes an instance of the EmailRecipient class.
        /// </summary>
        /// <param name="address">The address of the recipient.</param>
        /// <param name="name">The name of the recipient.</param>
        public EmailRecipient(string address, string name) : this(address)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the address of the email recipient.
        /// </summary>
        public string Address { set; get; }

        /// <summary>
        /// Gets or sets the name of the email recipient.
        /// </summary>
        public string Name { set; get; }

        internal string ToMailString()
        {
            StringBuilder builder = new StringBuilder();

            if(!string.IsNullOrEmpty(Name))
            {
                builder.Append(Name + " <");
            }

            builder.Append(Address);

            if (!string.IsNullOrEmpty(Name))
            {
                builder.Append(">");
            }

            return builder.ToString();
        }
    }
}
