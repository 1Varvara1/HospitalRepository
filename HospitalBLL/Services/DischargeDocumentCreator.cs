using HospitalBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Services
{
    public class DischargeDocumentCreator
    {
        private string TemplatePath;
        private ITreatmentService TreatmentService;
        private int IdDischarge;
        public DischargeDocumentCreator(string templatePath, ITreatmentService treatmentService,
            int idDischarge)
        {
            TemplatePath = templatePath;
            TreatmentService = treatmentService;
        }



    }
}
