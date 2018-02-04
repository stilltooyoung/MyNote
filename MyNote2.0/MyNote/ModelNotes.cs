namespace MyNote
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelNotes : DbContext
    {
        public ModelNotes()
            : base("ModelNotesContext")
        {
        }

        public virtual DbSet<Attention> Attentions { get; set; }
        public virtual DbSet<Diary> Diaries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
