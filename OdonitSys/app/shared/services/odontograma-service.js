app.factory('OdontogramaService', ['ToothServiceModel', function (ToothServiceModel) {
    return {       
        serviceSelected: '',
        personId: null,        
        servicesCollection: [],
        setPersonId:function(personId){
            this.personId = personId;
        },
        createService: function () {
            var that = this;
            if (!this.servicesTemp.length) { return; }
            //Iniciliaza el array definitivo con el ultimo item del array temporal
            this.servicesFinish.push(this.servicesTemp[0]);
            angular.forEach(this.servicesTemp, function (valTemp, index) {
                var itShould = true;
                if (index !== 0) {
                    angular.forEach(that.servicesFinish, function (valFin) {
                        if (valTemp.toothId === valFin.toothId && valTemp.faceId === valFin.faceId) {
                            itShould = false;
                            return;
                        }
                    });
                    if (itShould) {
                        that.servicesFinish.push(valTemp);
                    };
                };
            });            
            angular.forEach(this.servicesFinish, function (value) {
                var toothService = new ToothServiceModel.ToothService(that.personId, value.toothId, value.faceId, value.serviceId);
                that.servicesCollection.push(toothService);
            });
            //Al finalizar de crear las colecciones  limpio las temporales            
            this.cleanServiceTemp();
            this.cleanServiceFinish();
        },     
        cleanCollection: function () {
            this.servicesCollection = [];
        },
        cleanServiceTemp: function () {
            this.servicesTemp = [];
        },
        cleanServiceFinish: function () {
            this.servicesFinish = [];
        },
        //Objetos temporales que se comparten con la directiva
        servicesTemp : [],
        servicesFinish : [],
        service: function (toothId, faceId, serviceId) {
            this.toothId = toothId,
            this.faceId = faceId,
            this.serviceId = serviceId
        }        
    }
}])