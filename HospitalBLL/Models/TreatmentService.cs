using HospitalBLL.Comparers;
using HospitalBLL.Interfaces;
using HospitalDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class TreatmentService : ITreatmentService
    {
        IUnitOfWork Database { get; set; }

        public TreatmentService(IUnitOfWork uow)
        {
            Database = uow;
        }

     

        public List<DrugBLL> GetAllDrags()
        {
            var drugs = Database.DrugsRepository.GetAll().ToList();
            var model = new List<DrugBLL>();
            foreach (var d in drugs)
            {
                model.Add(new DrugBLL(d.IdDrugs, d.DrugsName));
            }
            model.Sort(new DrugsNameComparer());

            return model;
        }

        public List<OperationBLL> GetOperations()
        {
            var operations = Database.OperationRepository.GetAll().ToList();
            var model = new List<OperationBLL>();
            foreach (var d in operations)
            {
                model.Add(new OperationBLL(d.IdOperation, d.OperationName));
            }
            model.Sort(new OperationNameComparer());

            return model;
        }

        public List<ProcedureBLL> GetProcedures()
        {
            var procedures = Database.ProcedureRepository.GetAll().ToList();
            var model = new List<ProcedureBLL>();
            foreach (var d in procedures)
            {
                model.Add(new ProcedureBLL(d.IdProcedure, d.ProcedureName));
            }
            model.Sort(new ProcedureNameComparer());

            return model;
        }

        public List<DiagnosisBLL> GetDiagnosis()
        {
            var diagnosises = Database.DiagnosisRepository.GetAll().ToList();
            var model = new List<DiagnosisBLL>();
            foreach (var d in diagnosises)
            {
                model.Add(new DiagnosisBLL
                {
                    IdDiagnosis = d.IdDiagnosis,
                    DiagnosisName = d.DiagnosisName
                });

            }
            model.Sort(new DiagnosisNameComparer());

            return model;
        }
    }
}
