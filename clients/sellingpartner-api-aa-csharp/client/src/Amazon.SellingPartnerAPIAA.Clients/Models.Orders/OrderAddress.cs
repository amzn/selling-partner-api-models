/* 
 * Selling Partner API for Orders
 *
 * The Selling Partner API for Orders helps you programmatically retrieve order information. These APIs let you develop fast, flexible, custom applications in areas like order synchronization, order research, and demand-based decision support tools.
 *
 * OpenAPI spec version: v0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = Amazon.SellingPartnerAPIAA.Clients.Client.SwaggerDateConverter;

namespace Amazon.SellingPartnerAPIAA.Clients.Models.Orders
{
    /// <summary>
    /// The shipping address for the order.
    /// </summary>
    [DataContract]
    public partial class OrderAddress :  IEquatable<OrderAddress>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderAddress" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected OrderAddress() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderAddress" /> class.
        /// </summary>
        /// <param name="amazonOrderId">An Amazon-defined order identifier, in 3-7-7 format. (required).</param>
        /// <param name="buyerCompanyName">Company name of the destination address..</param>
        /// <param name="shippingAddress">shippingAddress.</param>
        /// <param name="deliveryPreferences">deliveryPreferences.</param>
        public OrderAddress(string amazonOrderId = default(string), string buyerCompanyName = default(string), Address shippingAddress = default(Address), DeliveryPreferences deliveryPreferences = default(DeliveryPreferences))
        {
            // to ensure "amazonOrderId" is required (not null)
            if (amazonOrderId == null)
            {
                throw new InvalidDataException("amazonOrderId is a required property for OrderAddress and cannot be null");
            }
            else
            {
                this.AmazonOrderId = amazonOrderId;
            }
            this.BuyerCompanyName = buyerCompanyName;
            this.ShippingAddress = shippingAddress;
            this.DeliveryPreferences = deliveryPreferences;
        }
        
        /// <summary>
        /// An Amazon-defined order identifier, in 3-7-7 format.
        /// </summary>
        /// <value>An Amazon-defined order identifier, in 3-7-7 format.</value>
        [DataMember(Name="AmazonOrderId", EmitDefaultValue=false)]
        public string AmazonOrderId { get; set; }

        /// <summary>
        /// Company name of the destination address.
        /// </summary>
        /// <value>Company name of the destination address.</value>
        [DataMember(Name="BuyerCompanyName", EmitDefaultValue=false)]
        public string BuyerCompanyName { get; set; }

        /// <summary>
        /// Gets or Sets ShippingAddress
        /// </summary>
        [DataMember(Name="ShippingAddress", EmitDefaultValue=false)]
        public Address ShippingAddress { get; set; }

        /// <summary>
        /// Gets or Sets DeliveryPreferences
        /// </summary>
        [DataMember(Name="DeliveryPreferences", EmitDefaultValue=false)]
        public DeliveryPreferences DeliveryPreferences { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OrderAddress {\n");
            sb.Append("  AmazonOrderId: ").Append(AmazonOrderId).Append("\n");
            sb.Append("  BuyerCompanyName: ").Append(BuyerCompanyName).Append("\n");
            sb.Append("  ShippingAddress: ").Append(ShippingAddress).Append("\n");
            sb.Append("  DeliveryPreferences: ").Append(DeliveryPreferences).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as OrderAddress);
        }

        /// <summary>
        /// Returns true if OrderAddress instances are equal
        /// </summary>
        /// <param name="input">Instance of OrderAddress to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OrderAddress input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.AmazonOrderId == input.AmazonOrderId ||
                    (this.AmazonOrderId != null &&
                    this.AmazonOrderId.Equals(input.AmazonOrderId))
                ) && 
                (
                    this.BuyerCompanyName == input.BuyerCompanyName ||
                    (this.BuyerCompanyName != null &&
                    this.BuyerCompanyName.Equals(input.BuyerCompanyName))
                ) && 
                (
                    this.ShippingAddress == input.ShippingAddress ||
                    (this.ShippingAddress != null &&
                    this.ShippingAddress.Equals(input.ShippingAddress))
                ) && 
                (
                    this.DeliveryPreferences == input.DeliveryPreferences ||
                    (this.DeliveryPreferences != null &&
                    this.DeliveryPreferences.Equals(input.DeliveryPreferences))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.AmazonOrderId != null)
                    hashCode = hashCode * 59 + this.AmazonOrderId.GetHashCode();
                if (this.BuyerCompanyName != null)
                    hashCode = hashCode * 59 + this.BuyerCompanyName.GetHashCode();
                if (this.ShippingAddress != null)
                    hashCode = hashCode * 59 + this.ShippingAddress.GetHashCode();
                if (this.DeliveryPreferences != null)
                    hashCode = hashCode * 59 + this.DeliveryPreferences.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
