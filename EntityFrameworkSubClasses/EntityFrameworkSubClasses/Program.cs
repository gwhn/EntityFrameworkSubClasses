using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkSubClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new MyContext())
            {
                var @as = ctx.As;
                foreach (var a in @as)
                {
                    Console.WriteLine("A.Id={0}", a.Id);
                    Console.WriteLine("A.Name={0}", a.Name);
                    Console.WriteLine("B.Id={0}", a.B.Id);
                    Console.WriteLine("B.Name={0}", a.B.Name);
                    if (a.B is Bb)
                    {
                        var bb = a.B as Bb;
                        Console.WriteLine("A.Bb.SubName={0}", bb.SubName);
                        Console.WriteLine("A.Bb.D.Name={0}", bb.D.Name);
                        if (bb.D is Dd)
                        {
                            var dd = bb.D as Dd;
                            Console.WriteLine("A.Bb.Dd.Name={0}", dd.SubName);
                        }
                    }
                    if (a is Aa)
                    {
                        var aa = a as Aa;
                        Console.WriteLine("Aa.SubName={0}", aa.SubName);
                        Console.WriteLine("Aa.C.Id={0}", aa.C.Id);
                        Console.WriteLine("Aa.C.Name={0}", aa.C.Name);
                        if (aa.C is Cc)
                        {
                            var cc = aa.C as Cc;
                            Console.WriteLine("Aa.Cc.SubName={0}", cc.SubName);
                        }
                    }
                }
            }
        }
    }

    class MyContext : DbContext
    {
        public DbSet<A> As { get; set; }
        public DbSet<B> Bs { get; set; }
        public DbSet<C> Cs { get; set; }
        public DbSet<D> Ds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<A>()
                .ToTable("As")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<A>()
                .HasRequired(x => x.B)
                .WithMany(x => x.As)
                .HasForeignKey(x => x.BId);

            modelBuilder.Entity<Aa>()
                .ToTable("Aas")
                .HasKey(x => x.Id)
                .HasRequired(x => x.C)
                .WithMany(x => x.Aas)
                .HasForeignKey(x => x.CId);

            modelBuilder.Entity<B>()
                .ToTable("Bs")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<B>()
                .HasMany(x => x.As)
                .WithRequired(x => x.B)
                .HasForeignKey(x => x.BId);

            modelBuilder.Entity<Bb>()
                .ToTable("Bbs")
                .HasKey(x => x.Id)
                .HasRequired(x => x.D)
                .WithMany(x => x.Bbs)
                .HasForeignKey(x => x.DId);

            modelBuilder.Entity<C>()
                .ToTable("Cs")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<C>()
                .HasMany(x => x.Aas)
                .WithRequired(x => x.C)
                .HasForeignKey(x => x.CId);

            modelBuilder.Entity<Cc>()
                .ToTable("Ccs")
                .HasKey(x => x.Id);

            modelBuilder.Entity<D>()
                .ToTable("Ds")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<D>()
                .HasMany(x => x.Bbs)
                .WithRequired(x => x.D)
                .HasForeignKey(x => x.DId);

            modelBuilder.Entity<Dd>()
                .ToTable("Dds")
                .HasKey(x => x.Id);
        }
    }

    class A
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BId { get; set; }
        public B B { get; set; }
    }

    class Aa : A
    {
        public string SubName { get; set; }
        public int CId { get; set; }
        public C C { get; set; }
    }

    class B
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Collection<A> As { get; set; }
    }

    class Bb : B
    {
        public string SubName { get; set; }
        public int DId { get; set; }
        public D D { get; set; }
    }

    class C
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Collection<Aa> Aas { get; set; }
    }

    class Cc : C
    {
        public string SubName { get; set; }
    }

    class D
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Collection<Bb> Bbs { get; set; }
    }

    class Dd : D
    {
        public string SubName { get; set; }
    }
}
