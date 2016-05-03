app.factory('ToothServiceModel', ['globals', function (globals) {
    return {
        ToothService: function(personId,toothId,faceId,serviceId){            
            this.Diagnosis ='';        
            this.InitialDate = new Date();
            this.EndDate = null;
            this.Amount = 0;//Valor
            this.Payment = 0; //Abonado
            this.Remainder = 0;//Saldo
            this.PersonId = personId;
            this.ToothId = toothId || null;
            this.FaceId = faceId || null;
            this.ServiceId = serviceId || null;
            this.StatusId = 1;
            this.TreatmentId = null;
        }
    }
}])