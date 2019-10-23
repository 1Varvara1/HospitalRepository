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
        void MatchPatientDiagnosis(int idComplaint, int idDiagnosis);
    }
}
