using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Protective.Core.Attributes;

namespace Protective.Core.Entity.Authentication
{
    public class IdentityUser : IUser
    {
        public IdentityUser()
        {
            UserRoles = new List<IdentityRole>();
        }

        /// <summary>
        /// User ID that is required by the IUser inteface
        /// </summary>
        public string Id
        {
            get { return UserId.ToString(); }
            set
            {
                int userId = 0;
                int.TryParse(value, out userId);
                UserId = userId;
            }
        }

        public int UserId { get; set; }

        /// <summary>
        /// User's name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// User's firstname
        /// </summary>
        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }

        /// <summary>
        /// User's lastname
        /// </summary>
        [Required]
        [StringLength(128)]
        public string LastName { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        [Required]
        [StringLength(256)]
        [RegularExpression(Constants.EmailRegexPattern, ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// User's new password
        /// </summary>
        public string NewPasswordHash { get; set; }

        /// <summary>
        /// if the user has been deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// if the user has been approved
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// if the user has been locked out of the system
        /// </summary>
        public bool IsLockedOut { get; set; }

        /// <summary>
        /// if the user's password has been reset
        /// </summary>
        public bool PasswordReset { get; set; }

        /// <summary>
        /// User's creation date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// The Roles associated to the user
        /// </summary>
        [ValidateRole(1, ErrorMessage = "At least one role is required")]
        public List<IdentityRole> UserRoles { get; set; }
    }
}
