//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_User()
        {
            this.T_Student = new HashSet<T_Student>();
        }
    
        public int UID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPwd { get; set; }
        public string UsrPhone { get; set; }
        public string UserStatu { get; set; }
        public string UserType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Student> T_Student { get; set; }
    }
}