using HospitalDAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.EntityFramework
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base(conectionString) {
          
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Complaint> Complaints{ get; set; }
        public DbSet<Complaint_Doctor>Complaint_Doctors { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Doctor> Doctors{ get; set; }
        public DbSet<Drugs> Drugs { get; set; }
        public DbSet<DrugsPrescription> DrugsPrescriptions { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationPrescription> OperationPrescriptions { get; set; }
        public DbSet<Patient_Diagnosis> Patient_Diagnoses { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<ProcedurePrescription> ProcedurePrescriptions { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
       

        public DbSet<Discharge>Discharges { get; set; }

        //protected override void OnModelCreating(DbModelBuilder mb)
        //{
        //    mb.Entity<Complaint_Doctor>().HasRequired(cd => cd.Complaint).WithOptional(c=>c.Complaint_Doctor);
        //}

            public void ModelLoad()
        {
            
            ClientProfiles.Include(cp => cp.ApplicationUser);
            ClientProfiles.Include(cp => cp.Complaints);
            Complaints.Include(c => c.ClientProfile);
            Complaints.Include(c => c.Speciality);
            Complaint_Doctors.Include(cd => cd.Complaint);
            Complaint_Doctors.Include(cd => cd.Doctor);
            Discharges.Include(d => d.Complaint);
            Discharges.Include(d => d.Diagnosis);
            DrugsPrescriptions.Include(dp => dp.Complaint);
            DrugsPrescriptions.Include(dp => dp.Doctor);
            DrugsPrescriptions.Include(dp => dp.Drugs);
            OperationPrescriptions.Include(op => op.Complaint);
            OperationPrescriptions.Include(op => op.Doctor);
            OperationPrescriptions.Include(op => op.Operation);
            Patient_Diagnoses.Include(pd => pd.Complaint);
            Patient_Diagnoses.Include(pd => pd.Diagnosis);
            ProcedurePrescriptions.Include(pp => pp.Complaint);
            ProcedurePrescriptions.Include(pp => pp.Doctor);
            ProcedurePrescriptions.Include(pp => pp.Procedure);

            Doctors.Include(d => d.Speciality);
            Doctors.Include(d => d.ClientProfile);
            Doctors.Include(d => d.Complaint_Doctors);

            //modelBuilder.Entity<Complaint>().HasRequired(r => r.ClientProfile).WithMany().HasForeignKey(k=>k.IdComplaint).WillCascadeOnDelete();
            //modelBuilder.Entity<ClientProfile>().HasMany(m => m.Complaints).WithRequired().HasForeignKey(k=>k.IdClientProfile).WillCascadeOnDelete();
            //modelBuilder.Entity<Complaint>().HasRequired(r => r.Speciality).WithMany().HasForeignKey(k=>k.IdSpeciality);
            //modelBuilder.Entity<Speciality>().HasMany(m => m.Complaints);
            //modelBuilder.Entity<Doctor>().HasRequired(r => r.ClientProfile);
            //modelBuilder.Entity<Doctor>().HasRequired(r => r.Speciality).WithMany().HasForeignKey(k=>k.IdSpeciality);
            //modelBuilder.Entity<Doctor>().HasMany(r => r.Complaint_Doctors).WithRequired().HasForeignKey(k=>k.IdComplaint_Doctor);
            //modelBuilder.Entity<Speciality>().HasMany(m => m.Doctors);
            //modelBuilder.Entity<Complaint_Doctor>().HasRequired(m => m.Complaint).WithMany().HasForeignKey(k=>k.);
            //modelBuilder.Entity<Complaint_Doctor>().HasRequired(m => m.Doctor);
            //modelBuilder.Entity<Complaint_Doctor>().HasRequired(m => m.StatusComplaint);
            //modelBuilder.Entity<StatusComplaint>().HasMany(m => m.Complaints);
            //modelBuilder.Entity<Patient_Diagnosis>().HasRequired(m => m.Complaint);
            //modelBuilder.Entity<Patient_Diagnosis>().HasRequired(m => m.Diagnosis);
            //modelBuilder.Entity<Diagnosis>().HasMany(m => m.Patient_Diagnoses);
            //modelBuilder.Entity<ProcedurePrescription>().HasRequired(m => m.Complaint);
            //modelBuilder.Entity<Procedure>().HasMany(m => m.PocedurePrescriptions);
            //modelBuilder.Entity<ProcedurePrescription>().HasRequired(m => m.StatusPrescription);
            //modelBuilder.Entity<StatusPrescription>().HasMany(m => m.ProcedurePrescriptions);
            //modelBuilder.Entity<DrugsPrescription>().HasRequired(m => m.Complaint);
            //modelBuilder.Entity<DrugsPrescription>().HasRequired(m => m.Drugs);
            //modelBuilder.Entity<DrugsPrescription>().HasRequired(m => m.StatusPrescription);
            //modelBuilder.Entity<OperationPrescription>().HasRequired(m => m.StatusPrescription);
            //modelBuilder.Entity<OperationPrescription>().HasRequired(m => m.Operation);
            //modelBuilder.Entity<OperationPrescription>().HasRequired(m => m.Complaint);

            //modelBuilder.Entity<OperationPrescription>().HasOptional(op=>op.ClientProfile);
            //modelBuilder.Entity<DrugsPrescription>().HasOptional(op => op.ClientProfile);
            //modelBuilder.Entity<ProcedurePrescription>().HasOptional(op => op.ClientProfile);


        }
        }
}
