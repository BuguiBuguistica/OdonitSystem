var odontograma = angular.module('odontograma-module', []);

odontograma.directive('odontograma', ['OdontogramaService', '$compile', '$rootScope', '$filter', '$timeout', function (OdontogramaService, $compile, $rootScope, $filter, $timeout) {
    return {
            restrict: 'A',
            scope: {
                checkId: '@',
                quadrant:'@',
                selection:'='
            },
        replace:true,
        template: '<div class="content-piezaDental" >'+
                    '<div class="number-piezaDental">{{checkId}}</div>' +
                    '<div class="number-piezaDental glyphicon"  style="height:18px" id="{{reparacionSupId}}"><strong>{{reparacionSup}}</strong></div>' +
                    '<div style="position:relative;width:50px; height:50px">' +
                        '<div style="position:absolute; top:0; left:0; z-index:1; width:50px; height:50px">'+
                            '<div style="height:12px; margin-left:19px;  text-align:center; float:left;"><i id="{{fracturaSupId}}" class="hide glyphicon glyphicon-ok small" style="padding:0; line-height:9px; display:block"></i></div>' +
                            '<div style="position: absolute;height:12px; top:19px; left:19px; text-align:center; "><i id="{{fracturaCenId}}" class="hide glyphicon glyphicon-ok small" style="padding:0; line-height:9px; display:block"></i></div>'+
                            '<div style="width:100%;height:12px;float:left;margin-top:7px;"><div style="width:12px; text-align:center; clear:left; float:left;"><i id="{{fracturaIzqId}}" class=" hide glyphicon glyphicon-ok small" style="padding:0; line-height:9px; display:block"></i></div>' +
                            '<div style="width:12px; text-align:center;clear:right; float:right;"><i id="{{fracturaDrcId}}" class="hide glyphicon glyphicon-ok small" style="padding:0; line-height:9px; display:block"></i></div></div>' +
                            '<div style="height:12px; text-align:center; clear:both; margin-top:5px; margin-left:19px; float:left;"><i id="{{fracturaInfId}}" class="hide glyphicon glyphicon-ok small" style="padding:0; line-height:9px; display:block"></i></div>' +
                            '<div style="height:50px; width:50px; text-align:center; border:1px solid black; font-size:50px; position: absolute; top: 0;left: 0; line-height: 50px;" id="{{tratamientoPiezaId}}">{{tratamientoPieza}}</div>' +
                         '</div>' +
                         '<canvas id="{{checkId}}"></canvas>' +
                         '<canvas id="corona" style="position:absolute; top:0; left:0; z-index:2; "></canvas>' +
                     '</div>' +
                     '<div class="number-piezaDental glyphicon"  style="height:20px" id="{{reparacionInfId}}"><strong></strong></div>' +
                  '</div>',
        
        link: function ($scope, elem, attr) {
            
            //Limpia el odontograma despues de haber guardado
            $scope.$on('cleanOdontogram', function (event) {              
                cleanOdontogram();
            });
            
            function cleanOdontogram() {
                cleanCorona();
                cleanRepSup();
                cleanElemErupcionSup();
                cleanElemErupcionInf();
                clearAll();
                angular.forEach(fff, function (e) {
                    var c = e.getContext("2d");
                    c.clearRect(0, 0, 50, 50);
                });
                draw();
                cleanElemAusnteExtraccion();
                //Reset array de servicios temp
                OdontogramaService.servicesTemp = [];
            };
                
            $scope.fracturaSupId = 'fracturaSup' + $scope.checkId;
            $scope.fracturaIzqId = 'fracturaIzq' + $scope.checkId;
            $scope.fracturaDrcId = 'fracturaDrc' + $scope.checkId;
            $scope.fracturaInfId = 'fracturaInf' + $scope.checkId;
            $scope.fracturaCenId = 'fracturaCen' + $scope.checkId;
            //Tratamientos TC - Se - Erupcion (flechas)
            $scope.reparacionSup = '';
            //Tratamientos que incluyen a toda la pieza
            $scope.tratamientoPieza = ''
            $scope.tratamientoPiezaId = 'tratamientoPieza' + $scope.checkId;

            //Funcion para agregar al array temporal los servicios q se capturan
            function addTempService(faceId, serviceId) {
                var service = new OdontogramaService.service($scope.checkId, faceId, serviceId);
                OdontogramaService.servicesTemp.unshift(service);
            };

            $scope.fracturas = [
                $scope.fracturaSupId,
                $scope.fracturaIzqId,
                $scope.fracturaDrcId,
                $scope.fracturaInfId,
                $scope.fracturaCenId
            ];
            $scope.showFracSup = false;
            $scope.showFracInf = false;
            $scope.showFracIzq = false;
            $scope.showFracDrc = false;
            $scope.showFracCen = false;            
            $scope.reparacionSupId = "reparacionSup" + $scope.checkId;
            $scope.reparacionInfId = "reparacionInf" + $scope.checkId;
            //'fracturaar': 1,
            //'cariear': 2,
            //'extraccionar': 3,
            //'coronaar': 4,
            //'tratamientoConductoar':5,                
            //'restauracionr': 6,
            //'extraccionr': 7,
            //'elementoAusenter': 8,
            //'coronar': 9,
            //'sellanter': 10,
            //'tratamientoConductor': 11,
            //'elementoEnErupcionSupr': 12,
            //'elementoEnErupcionInfr': 13,
            
            var caraIzq, caraSup, caraDrc, caraInf, caraCen, canvasCaries = elem[0].children[2].children[1],
                canvasCorona,
                v1 = 0, v2 = 14, v3 = 36, v4 = 50, //coordenadas
                red = "#FF0000", blue = "#40ADDD";
            var fff = [];
            fff.push(canvasCaries);
            //canvas settings
            canvasCaries.width = 50;
            canvasCaries.height = 50;

            //constructor caras
            function Canvas(canvas) {
                this.canvas = canvas.getContext("2d");
                this.width = 50;
                this.height = 50;
                this.clear = true;
            }
            //creando las caras
            caraIzq = new Canvas(canvasCaries);
            caraSup = new Canvas(canvasCaries);
            caraDrc = new Canvas(canvasCaries);
            caraInf = new Canvas(canvasCaries);
            caraCen = new Canvas(canvasCaries);

            //Prestaciones
            canvasCorona = elem[0].children[2].children[2];
            var corona = new Canvas(canvasCorona);
                           
            //draw odontograma
            draw();
            
            //Captura event click
            elem[0].children[2].addEventListener("click", halmaOnClick, false);
            function halmaOnClick(e) {
                var mouseX = e.pageX - this.offsetLeft,
                mouseY = e.pageY - this.offsetTop;
                console.log(mouseX, mouseY)
                switch ($scope.selection) {
                    case 1:
                        drawFracturas(mouseX, mouseY, blue);
                        break;
                    case 2:
                        drawCaries(mouseX, mouseY, blue);
                        break;
                    case 3:
                        extraccionARealizar();
                        break;
                    case 4:
                        drawCorona(blue);
                        break;
                    case 5:
                        drawTCaRealizar();
                        break;                        
                    case 6:
                        drawCaries(mouseX, mouseY, red);
                        break;
                    case 7:
                        extraccionRealizada();
                        break;
                    case 8:
                        elementoAusente();
                        break;
                    case 9:
                        drawCorona(red);
                        break;
                    case 10:
                        drawSellante();
                        break;
                    case 11:
                        drawTCaRealizado();
                        break;
                    case 12:
                        elementoEnErupcionSup();
                        break;
                    case 13:
                        elementoEnErupcionInf();
                        break;
                    default:
                        cleanTooth();
                }                    
            }
            function cleanTooth() {
                //remover dibujo fracturas
                cleanFracturas();
                //remover dibujo caries
                cleanCaries();
                //Remover dibujo elemento ausente y extracciones
                cleanElemAusnteExtraccion();
                //Remover dibujo corona
                cleanCorona();
                //Remover dibujo Tratamiento de conducto
                cleanRepSup();
                //Remover dibujo elementos en erupcion
                cleanElemErupcionSup();
                cleanElemErupcionInf();
                console.log(OdontogramaService.servicesTemp);
                //Remover de los servicios a guardar                
                var itemsToRemove = $filter('filter')(OdontogramaService.servicesTemp, { toothId: $scope.checkId });
                angular.forEach(itemsToRemove, function (value, index) {
                    OdontogramaService.servicesTemp.splice(OdontogramaService.servicesTemp[value], 1);
                });
                console.log(OdontogramaService.servicesTemp);
            };
            
            ///////////////////////////////////////ELEMENTO AUSENTE/////////////////////////////////            
            //CREACION
            $scope.showElemAusente = true;
            $scope.showExtraccion = true;
            $scope.showExtraccionRealizada = true;
            function elementoAusente() {
                if ($scope.showElemAusente && $scope.showExtraccion && $scope.showExtraccionRealizada) {
                    var faceId = null, serviceId = 8;
                    var x = $("#" + $scope.tratamientoPiezaId);
                    x.addClass('red');
                    $timeout(function () {
                        $scope.tratamientoPieza = 'A';
                    }, 0);
                    addTempService(faceId, serviceId);
                    $scope.showElemAusente = false;
                };
            };
            //ELMINACION la misma function que para extraccion                
            ///////////////////////////////////////END ELEMENTO AUSENTE/////////////////////////////////

            ////////////////////////////////EXTRACCION////////////////////////////////////            
            //CREACION
            //A realizar            
            function extraccionARealizar() {
                if ($scope.showElemAusente && $scope.showExtraccion && $scope.showExtraccionRealizada) {
                    var faceId = null, serviceId = 3;
                    var x = $("#" + $scope.tratamientoPiezaId);
                    x.removeClass('red');
                    x.addClass('blue');
                    $scope.tratamientoPieza = 'X';
                    $scope.$apply();                    
                    addTempService(faceId, serviceId);
                    $scope.showExtraccion = false;
                };
            };                
            //Realizada            
            function extraccionRealizada() {                
                if ($scope.showElemAusente && $scope.showExtraccion && $scope.showExtraccionRealizada) {
                    var faceId = null, serviceId = 7;
                    var x = $("#" + $scope.tratamientoPiezaId);
                    x.removeClass('blue');
                    x.addClass('red');
                    $scope.tratamientoPieza = 'X';
                    $scope.$apply();                    
                    addTempService(faceId, serviceId);
                    $scope.showExtraccionRealizada = false;
                };
            };
            //ELIMINACION
            function cleanElemAusnteExtraccion() {                
                $timeout(function () {
                    $scope.tratamientoPieza = '';
                }, 0);
                $scope.showExtraccion = true;
                $scope.showExtraccionRealizada = true;
                $scope.showElemAusente = true;
            };
            ////////////////////////////////END EXTRACCION////////////////////////////////////

            /////////////////////////////////ELEMENTO EN ERUPCION/////////////////////////////
            //SUPERIOR
            $scope.showElemEnErupcionSup = true;
            function elementoEnErupcionSup() {
                if ($scope.showElemEnErupcionSup) {
                    var faceId = null, serviceId = 12;
                    var x = $("#" + $scope.reparacionSupId);
                    x.removeClass('blue');
                    x.addClass('red glyphicon-arrow-down');
                    addTempService(faceId, serviceId);
                    $scope.showElemEnErupcionSup = false;
                };
            };
            //ELIMINACION
            function cleanElemErupcionSup() {
                var x = $("#" + $scope.reparacionSupId);
                x.removeClass('red glyphicon-arrow-down');
                $scope.showElemEnErupcionSup = true;
            };
            //INFERIOR
            $scope.showeElemEnErupcionInf = true;
            function elementoEnErupcionInf() {
                if ($scope.showeElemEnErupcionInf) {
                    var faceId = null, serviceId = 13;
                    var x = $("#" + $scope.reparacionInfId);
                    x.removeClass('blue');
                    x.addClass('red glyphicon-arrow-up');
                    addTempService(faceId, serviceId);
                    $scope.showeElemEnErupcionInf = false;
                };
            };
            //ELIMINACION
            function cleanElemErupcionInf() {
                var x = $("#" + $scope.reparacionInfId);
                x.removeClass('red glyphicon-arrow-up');
                $scope.showeElemEnErupcionInf = true;
            };
            ////////////////////////////////TRATAMIENTO DE CONDUCTO////////////////////////////////
            //A Realizar
            //CREACION
            $scope.showTC = true;
            $scope.showTCr = true;
            $scope.showSellante = true;
            function drawTCaRealizar() {
                if ($scope.showTCr && $scope.showTC && $scope.showSellante) {
                    var faceId = null, serviceId = 5;
                    var x = $("#" + $scope.reparacionSupId);
                    x.removeClass('red');
                    x.addClass('blue');
                    $timeout(function () {
                        $scope.reparacionSup = 'TC';
                    }, 0);
                    addTempService(faceId, serviceId);
                    $scope.showTC = false;
                };
            };
            //Realizado            
            function drawTCaRealizado() {
                if ($scope.showTCr && $scope.showTC && $scope.showSellante) {
                    var faceId = null, serviceId = 11;
                    var x = $("#" + $scope.reparacionSupId);
                    x.removeClass('blue');
                    x.addClass('red');
                    $timeout(function () {
                        $scope.reparacionSup = 'TC';
                    }, 0);
                    addTempService(faceId, serviceId);
                    $scope.showTCr = false;
                };
            };
            //ELIMINACION
            function cleanRepSup() {
                $timeout(function () {
                    $scope.reparacionSup = '';
                });
                $scope.showTC = true;
                $scope.showTCr = true;
                $scope.showSellante = true;
            };            
            ////////////////////////////////END TRATAMIENTO DE CONDUCTO////////////////////////////////

            ///////////////////////////////SELLANTE////////////////////////
            //CREACION            
            function drawSellante() {
                if ($scope.showTCr && $scope.showTC && $scope.showSellante) {
                    var faceId = null, serviceId = 10;
                    var x = $("#" + $scope.reparacionSupId);
                    x.removeClass('blue');
                    x.addClass('red');
                    $timeout(function () {
                        $scope.reparacionSup = 'Se';
                    }, 0);
                    addTempService(faceId, serviceId);
                    $scope.showSellante = false;
                };
            };
            //ELMINACION la misma function que Tratamiento de conducta
            //////////////////////////END SELLANTE/////////////////////////

            /////////////////////////////CORONA//////////////////////////
            //CREACTION
            $scope.haveCorona = false;
            function drawCorona(color) {
                if (!$scope.haveCorona) {
                    var faceId = null;
                    var serviceId = color === 'blue' ? 4 : 9;

                    var x = corona.width / 2;
                    var y = corona.height / 2;
                    var radius = 23;

                    corona.canvas.beginPath();
                    corona.canvas.arc(x, y, radius, 0, 2 * Math.PI, false);
                    corona.canvas.lineWidth = 2;

                    corona.canvas.strokeStyle = color;
                    corona.canvas.stroke();
                    corona.canvas.closePath();

                    addTempService(faceId, serviceId);
                    $scope.haveCorona = true;
                };
            };
            //ELIMINACION
            function cleanCorona() {
                corona.canvas.clearRect(0, 0, 50, 50);
                $scope.haveCorona = false;
            }
            /////////////////////////////END CORONA//////////////////////////

            ////////////////////////////////CARIES///////////////////////////////////////
            //CREACION
            function drawCaries(x, y, color) {
                var faceId = null;
                var serviceId = color === 'blue' ? 2 : 6;

                //cara izquierda
                if (x <= v2) {                   
                    if (y <= v4 - x && y >= x) {
                        faceId = $scope.quadrant === '1' || $scope.quadrant === '2' ? 4 : 8;
                        drawCaraIzquierda();
                        var elem = $("#" + $scope.fracturaIzqId);
                        if (caraIzq.clear) {
                            console.log('cara izq')
                            caraIzq.canvas.fillStyle = color
                            caraIzq.canvas.fill();
                            caraIzq.canvas.stroke();
                            //Agregar el servicio al array de temporales
                            addTempService(faceId, serviceId);
                            caraIzq.clear = false;
                        };
                    };
                };

                //cara superior
                if (y <= v2) {                   
                    if (x <= v4 - y && x >= y) {
                        faceId = $scope.quadrant === '1' || $scope.quadrant === '2' ? 1 : 5;
                        drawCaraSuperior();
                        if (caraSup.clear) {
                            console.log('cara superior')
                            caraSup.canvas.fillStyle = color;
                            caraSup.canvas.fill();
                            caraSup.canvas.stroke();
                            //Agregar el servicio al array de temporales
                            addTempService(faceId, serviceId);
                            caraSup.clear = false;
                        };
                    };
                };

                //cara derecha
                if (x >= v3) {                   
                    var cons = x - v3
                    if (y <= v4 - cons && y >= cons) {
                        faceId = $scope.quadrant === '1' || $scope.quadrant === '2' ? 2 : 6;
                        drawCaraDerecha();
                        if (caraDrc.clear) {
                            console.log('cara derecha')
                            caraDrc.canvas.fillStyle = color;
                            caraDrc.canvas.fill();
                            //Agregar el servicio al array de temporales
                            addTempService(faceId, serviceId);
                            caraDrc.clear = false;
                        };
                    };
                };

                //cara inferior
                if (y >= v3) {                    
                    var cons = y - v3
                    if (x <= v4 - cons && x >= cons) {
                        faceId = $scope.quadrant === '1' || $scope.quadrant === '2' ? 3 : 7;
                        drawCaraInferior();
                        if (caraInf.clear) {
                            console.log('cara inferior')
                            caraInf.canvas.fillStyle = color;
                            caraInf.canvas.fill();
                            //Agregar el servicio al array de temporales
                            addTempService(faceId, serviceId);
                            caraInf.clear = false;
                        };
                    };
                };

                //cara central
                if ((y >= v2 && y <= v3) && (x >= v2 && x <= v3)) {
                    faceId = 9;
                    if (caraCen.clear) {
                        console.log('cara central');
                        caraCen.canvas.fillStyle = color;
                        caraCen.canvas.fill();
                        //Agregar el servicio al array de temporales
                        addTempService(faceId, serviceId);
                        caraCen.clear = false;
                    };
                };
            };
            //ELIMINACION
            function cleanCaries() {
                var canvas = canvasCaries.getContext("2d");
                canvas.clearRect(0, 0, 50, 50);
                draw();
                caraIzq.clear = true;
                caraDrc.clear = true;
                caraSup.clear = true;
                caraInf.clear = true;
                caraCen.clear = true;
            };
            //////////////////////////////////END CARIES///////////////////////////////////////////

            function drawCaraIzquierda() {
                caraIzq.canvas.beginPath();
                caraIzq.canvas.moveTo(v1, v1);
                caraIzq.canvas.lineTo(v2, v2);
                caraIzq.canvas.lineTo(v2, v3);
                caraIzq.canvas.lineTo(v1, v4);
                caraIzq.canvas.lineTo(v1, v1);
                caraIzq.canvas.lineWidth = 1;
                caraIzq.canvas.stroke();                    
            }

            function drawCaraDerecha() {
                caraDrc.canvas.beginPath();
                caraDrc.canvas.moveTo(v4, v1);
                caraDrc.canvas.lineTo(v3, v2);
                caraDrc.canvas.lineTo(v3, v3);
                caraDrc.canvas.lineTo(v4, v4);
                caraDrc.canvas.lineTo(v4, v1);
                caraDrc.canvas.lineWidth = 1;
                caraDrc.canvas.stroke();  
            }

            function drawCaraSuperior() {
                caraSup.canvas.beginPath();
                caraSup.canvas.moveTo(v1, v1);
                caraSup.canvas.lineTo(v2, v2);
                caraSup.canvas.lineTo(v3, v2);
                caraSup.canvas.lineTo(v3, v2);
                caraSup.canvas.lineTo(v4, v1);
                caraSup.canvas.lineTo(v1, v1);
                caraSup.canvas.lineWidth = 1;
                caraSup.canvas.stroke();
            }


            function drawCaraInferior() {
                caraInf.canvas.beginPath();
                caraInf.canvas.moveTo(v1, v4);
                caraInf.canvas.lineTo(v2, v3);
                caraInf.canvas.lineTo(v3, v3);
                caraInf.canvas.lineTo(v4, v4);
                caraInf.canvas.lineTo(v1, v4);
                caraInf.canvas.lineWidth = 1;
                caraInf.canvas.stroke();
            }

            function drawCaraCentral() {
                caraCen.canvas.beginPath();
                caraCen.canvas.rect(14, 14, 22, 22);
                caraCen.canvas.lineWidth = 1;
                caraCen.canvas.stroke();
            }

            function draw() {
                //Dibujo base odontograma                   
                drawCaraIzquierda();
                drawCaraDerecha();
                drawCaraSuperior();
                drawCaraInferior();
                drawCaraCentral();
            }            
                
            //Limpiar las caras de las piezas
            function clearAll() {
                cleanCorona();

                //limpieza caries
                cleanCaraSuperior();
                cleanCaraIzquierda();
                cleanCaraDerecha();
                cleanCaraInferior();
                cleanCaraCentral();

                //limpiezaFractura                   
                angular.forEach($scope.fracturas, function (value) {
                    x = $('#' + value);
                    x.removeClass('show');
                    x.addClass('hide');
                })
                $scope.showFracInf = false;
                $scope.showFracSup = false;
                $scope.showFracIzq = false;
                $scope.showFracDrc = false;
                $scope.showFracCen = false;
            };

            function cleanCaraSuperior() {
                caraSup.clear = true;
                caraSup.canvas.fillStyle = 'white';
                caraSup.canvas.fill();
                caraSup.canvas.stroke();
            };
            function cleanCaraIzquierda() {
                caraIzq.clear = true;
                caraIzq.canvas.fillStyle = 'white';
                caraIzq.canvas.fill();
                caraIzq.canvas.stroke();
            };
            function cleanCaraDerecha() {
                caraDrc.clear = true;
                caraDrc.canvas.fillStyle = 'white';
                caraDrc.canvas.fill();
                caraDrc.canvas.stroke();
            };
            function cleanCaraInferior() {
                caraInf.clear = true;
                caraInf.canvas.fillStyle = 'white';
                caraInf.canvas.fill();
                caraInf.canvas.stroke();
            };
            function cleanCaraCentral() {
                caraCen.clear = true;
                caraCen.canvas.fillStyle = 'white';
                caraCen.canvas.fill();
                caraCen.canvas.stroke();
            };
                
            
            /////////////////////////////////////FRACTURAS//////////////////////////////////
            //CREACION
            function drawFracturas(x, y, color) {
                var faceId = null;
                var serviceId = 1;

                //cara izquierda
                if (x <= v2) {
                    if (!$scope.showFracIzq) {
                        var el = $("#" + $scope.fracturaIzqId);                        
                        el.addClass('show');
                        faceId = $scope.quadrant === '1' || $scope.quadrant === '2' ? 4 : 8;
                        addTempService(faceId, serviceId);
                        $scope.showFracIzq = true;
                    };
                }

                //cara superior
                if (y <= v2) {
                    if (!$scope.showFracSup) {
                        var el = $("#" + $scope.fracturaSupId);
                        el.addClass('show');
                        faceId = $scope.quadrant === '1' || $scope.quadrant === '2' ? 1 : 5;
                        addTempService(faceId, serviceId);
                        $scope.showFracSup = true;
                    };
                };

                //cara derecha
                if (x >= v3) {
                    if (!$scope.showFracDrc) {
                        var el = $("#" + $scope.fracturaDrcId);                       
                        el.addClass('show');
                        faceId = $scope.quadrant === '1' || $scope.quadrant === '2' ? 2 : 6;
                        addTempService(faceId, serviceId);  
                        $scope.showFracDrc = true;
                    };
                };

                //cara inferior
                if (y >= v3) {
                    var cons = y - v3
                    if (x <= v4 - cons && x >= cons) {
                        if (!$scope.showFracInf) {
                            var el = $("#" + $scope.fracturaInfId);                            
                            el.addClass('show');
                            faceId = $scope.quadrant === '1' || $scope.quadrant === '2' ? 3 : 7;
                            addTempService(faceId, serviceId);
                            $scope.showFracInf = true;
                        };
                    };
                };

                //cara central
                if ((y >= v2 && y <= v3) && (x >= v2 && x <= v3)) {
                    if (!$scope.showFracCen) {
                        var el = $("#" + $scope.fracturaCenId);                        
                        el.addClass('show');
                        faceId = 9;
                        addTempService(faceId, serviceId);                        
                        $scope.showFracCen = true;
                    };
                };
            };
            //ELIMINACION 
            function cleanFracturas() {
                //Izq
                var el = $("#" + $scope.fracturaIzqId);
                el.removeClass('opacity');
                el.removeClass('show');
                $scope.showFracIzq = false;
                //Drc
                var el = $("#" + $scope.fracturaDrcId);
                el.removeClass('opacity');
                el.removeClass('show');
                $scope.showFracDrc = false;
                //Sup
                var el = $("#" + $scope.fracturaSupId);
                el.removeClass('opacity');
                el.removeClass('show');
                $scope.showFracSup = false;
                //Inf
                var el = $("#" + $scope.fracturaInfId);
                el.removeClass('opacity');
                el.removeClass('show');
                $scope.showFracInf = false;
                //Cen
                var el = $("#" + $scope.fracturaCenId);
                el.removeClass('opacity');
                el.removeClass('show');
                $scope.showFracCen = false;
            };
            /////////////////////////////////////////////END FRACTURAS//////////////////////////////////////////

        }            
    }
}])