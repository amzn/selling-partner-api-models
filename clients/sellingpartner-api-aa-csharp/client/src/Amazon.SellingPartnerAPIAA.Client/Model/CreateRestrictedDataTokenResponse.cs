/* 
 * Selling Partner API for Tokens 
 *
 * The Selling Partner API for Tokens provides a secure way to access a customer's PII (Personally Identifiable Information). You can call the Tokens API to get a Restricted Data Token (RDT) for one or more restricted resources that you specify. The RDT authorizes subsequent calls to restricted operations that correspond to the restricted resources that you specified.  For more information, see the [Tokens API Use Case Guide](doc:tokens-api-use-case-guide).
 *
 * OpenAPI spec version: 2021-03-01
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
using SwaggerDateConverter = Amazon.SellingPartnerAPIAA.Client.Client.SwaggerDateConverter;

namespace Amazon.SellingPartnerAPIAA.Client.Model
{
    /// <summary>
    /// The response schema for the createRestrictedDataToken operation.
    /// </summary>
    [DataContract]
    public partial class CreateRestrictedDataTokenResponse :  IEquatable<CreateRestrictedDataTokenResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRestrictedDataTokenResponse" /> class.
        /// </summary>
        /// <param name="restrictedDataToken">A Restricted Data Token (RDT). This is a short-lived access token that authorizes calls to restricted operations. Pass this value with the x-amz-access-token header when making subsequent calls to these restricted resources..</param>
        /// <param name="expiresIn">The lifetime of the Restricted Data Token, in seconds..</param>
        public CreateRestrictedDataTokenResponse(string restrictedDataToken = default(string), int? expiresIn = default(int?))
        {
            this.RestrictedDataToken = restrictedDataToken;
            this.ExpiresIn = expiresIn;
        }
        
        /// <summary>
        /// A Restricted Data Token (RDT). This is a short-lived access token that authorizes calls to restricted operations. Pass this value with the x-amz-access-token header when making subsequent calls to these restricted resources.
        /// </summary>
        /// <value>A Restricted Data Token (RDT). This is a short-lived access token that authorizes calls to restricted operations. Pass this value with the x-amz-access-token header when making subsequent calls to these restricted resources.</value>
        [DataMember(Name="restrictedDataToken", EmitDefaultValue=false)]
        public string RestrictedDataToken { get; set; }

        /// <summary>
        /// The lifetime of the Restricted Data Token, in seconds.
        /// </summary>
        /// <value>The lifetime of the Restricted Data Token, in seconds.</value>
        [DataMember(Name="expiresIn", EmitDefaultValue=false)]
        public int? ExpiresIn { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CreateRestrictedDataTokenResponse {\n");
            sb.Append("  RestrictedDataToken: ").Append(RestrictedDataToken).Append("\n");
            sb.Append("  ExpiresIn: ").Append(ExpiresIn).Append("\n");
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
            return this.Equals(input as CreateRestrictedDataTokenResponse);
        }

        /// <summary>
        /// Returns true if CreateRestrictedDataTokenResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of CreateRestrictedDataTokenResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CreateRestrictedDataTokenResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.RestrictedDataToken == input.RestrictedDataToken ||
                    (this.RestrictedDataToken != null &&
                    this.RestrictedDataToken.Equals(input.RestrictedDataToken))
                ) && 
                (
                    this.ExpiresIn == input.ExpiresIn ||
                    (this.ExpiresIn != null &&
                    this.ExpiresIn.Equals(input.ExpiresIn))
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
                if (this.RestrictedDataToken != null)
                    hashCode = hashCode * 59 + this.RestrictedDataToken.GetHashCode();
                if (this.ExpiresIn != null)
                    hashCode = hashCode * 59 + this.ExpiresIn.GetHashCode();
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
