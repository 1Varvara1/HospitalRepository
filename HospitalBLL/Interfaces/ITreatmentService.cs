using HospitalBLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Interfaces
{
    public interface ITreatmentService:IOperationService, IProcedureService, IDrugsService, IDiagnosisService
    {
        void MatchPatientDiagnosis(int idComplaint, int idDiagnosis );
        void AddProcedurePrescriptionPatient(ProcedurePrescriptonBLL procPrescr );
        void AddDrugPrescriptionPatient(DrugPrescriptionBLL dPrescr);
        void AddOperationPrescriptionPatient(OperationPrescriptionsBLL opPrescr);
        void CompleteDrugPrescription(int idDrugs, int idComplaint, string idDoctor);
        void CompleteProcedurePrescription(int procedureId, int idComplaint, string idDoctor);
        void CompleteOperationPrescription(int operationId, int idComplaint, string idDoctor);
        void DischagePatient(int idComplaint, string recomendations, int IdDiagnosis);

        List<SessionBLL> GetPatientTreatmentHistory(string idPatient);
    }
}
