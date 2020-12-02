﻿using core.Entities.MasterData;
using core.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;

namespace core.UseCase.InitDatabase
{
    public class InitDb
    {
      
        public bool InitDatabase()
        {
            using var db = new dbContext();
            //db.Database.EnsureDeleted();
            if (!((RelationalDatabaseCreator) db.Database.GetService<IDatabaseCreator>()).Exists())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.EntidadesModel.AddRange(UploadEntidades());
                db.FalabellaModel.AddRange(UploadFalabella());
                db.CnbsModel.AddRange(UploadCnbs());
                db.RedprivadasModel.AddRange(Uploadredprivadas());
                db.ConveniosModel.AddRange(Uploadconvenios());
                db.SaveChanges();
            }

            return true;
        }

        private List<EntidadesModel> UploadEntidades() 
        {
            var json = @"[{'FIID':'0832','NOMBRE':'NUEVA ENTIDAD            ','NIT':'                         '},
                          {'FIID':'0059','NOMBRE':'NUEVA ENTIDAD            ','NIT':'                         '},
                          {'FIID':'0200','NOMBRE':'NUEVA ENTIDAD            ','NIT':'                         '},
                          {'FIID':'0833','NOMBRE':'NUEVA ENTIDAD            ','NIT':'                         '},
                          {'FIID':'0838','NOMBRE':'FOC                      '},
                          {'FIID':'    ','NOMBRE':'                         '},
                          {'FIID':'0001','NOMBRE':'BANCO DE BOGOTA          ','NIT':'8600029644               '},
                          {'FIID':'0002','NOMBRE':'BANCO POPULAR            ','NIT':'8600077389               '},
                          {'FIID':'0003','NOMBRE':'BANCO DE COLOMBIA        ','NIT':'8909039388               '},
                          {'FIID':'0005','NOMBRE':'BANCAFE                  ','NIT':'8600343137               '},
                          {'FIID':'0006','NOMBRE':'BANCO SANTANDER          ','NIT':'8909039370               '},
                          {'FIID':'0007','NOMBRE':'BANCOLOMBIA              ','NIT':'8909039388               '},
                          {'FIID':'0009','NOMBRE':'CITIBANK                 ','NIT':'8600511354               '},
                          {'FIID':'0010','NOMBRE':'BANCO GNB COLOMBIA       ','NIT':'8600509309               '},
                          {'FIID':'0011','NOMBRE':'CREDIBANCO               ','NIT':'8600329097               '},
                          {'FIID':'0013','NOMBRE':'BBVA COLOMBIA            ','NIT':'8600030201               '},
                          {'FIID':'0014','NOMBRE':'HELM BANK                ','NIT':'8600076603               '},
                          {'FIID':'0020','NOMBRE':'ESTADO                   ','NIT':'8915000159               '},
                          {'FIID':'0023','NOMBRE':'BANCO DE OCCIDENTE       ','NIT':'8903002794               '},
                          {'FIID':'0032','NOMBRE':'CAJA SOCIAL              ','NIT':'8600073350               '},
                          {'FIID':'0034','NOMBRE':'BANSUPERIOR              ','NIT':'8600343137               '},
                          {'FIID':'0035','NOMBRE':'MACROFINANCIERA          ','NIT':'8600244141               '},
                          {'FIID':'0036','NOMBRE':'MEGABANCO                ','NIT':'8600349215               '},
                          {'FIID':'0043','NOMBRE':'BANCO AGRARIO            ','NIT':'8000378008               '},
                          {'FIID':'0051','NOMBRE':'DAVIVIENDA CR            ','NIT':'8600343137               '},
                          {'FIID':'0060','NOMBRE':'PICHINCHA                ','NIT':'8902007567               '},
                          {'FIID':'0062','NOMBRE':'COLTEFINANCIERA          ','NIT':'8909270349               '},
                          {'FIID':'0073','NOMBRE':'SERVIBANCA               ','NIT':'8300366457               '},
                          {'FIID':'0082','NOMBRE':'BANCO CAJA SOCIAL        ','NIT':'8600073350               '},
                          {'FIID':'0102','NOMBRE':'FINAMERICA               ','NIT':'8600259715               '},
                          {'FIID':'0110','NOMBRE':'CONAVI                   ','NIT':'8909039388               '},
                          {'FIID':'0115','NOMBRE':'AHORRAMAS                ','NIT':'8600358275               '},
                          {'FIID':'0120','NOMBRE':'BCH                      ','NIT':'8600029637               '},
                          {'FIID':'0130','NOMBRE':'GRANAHORRAR              ','NIT':'8600030201               '},
                          {'FIID':'0136','NOMBRE':'MEGABANCO CR             ','NIT':'8600029644               '},
                          {'FIID':'0140','NOMBRE':'AVVILLAS                 ','NIT':'8600358275               '},
                          {'FIID':'0150','NOMBRE':'CONCASA                  ','NIT':'8600343137               '},
                          {'FIID':'0151','NOMBRE':'PROCREDIT                ','NIT':'9002009609               '},
                          {'FIID':'0160','NOMBRE':'DAVIVIENDA               ','NIT':'8600343137               '},
                          {'FIID':'0170','NOMBRE':'CORPAVI                  ','NIT':'8600345941               '},
                          {'FIID':'0180','NOMBRE':'COLMENA                  ','NIT':'8600073350               '},
                          {'FIID':'0190','NOMBRE':'COLPATRIA                ','NIT':'8600345941               '},
                          {'FIID':'0191','NOMBRE':'CONVENIOS                ','NIT':'8300705271               '},
                          {'FIID':'0198','NOMBRE':'INTERNACIONAL            '},
                          {'FIID':'0199','NOMBRE':'RBM                      ','NIT':'8300705271               '},
                          {'FIID':'0205','NOMBRE':'CONFIAR                  ','NIT':'8909813951               '},
                          {'FIID':'0360','NOMBRE':'DINERS CLUB              '},
                          {'FIID':'0408','NOMBRE':'GIROS Y FINANZAS         ','NIT':'8600067979               '},
                          {'FIID':'0423','NOMBRE':'SERFINANSA               ','NIT':'8600431866               '},
                          {'FIID':'0426','NOMBRE':'TUYA                     ','NIT':'8600323303               '},
                          {'FIID':'0446','NOMBRE':'AS400 COLTEFINANCIERA    ','NIT':'8909270349               '},
                          {'FIID':'0802','NOMBRE':'HSBC                     ','NIT':'8600509309               '},
                          {'FIID':'0804','NOMBRE':'AS400                    ','NIT':'8300705271               '},
                          {'FIID':'0806','NOMBRE':'NTS                      ','NIT':'8300705271               '},
                          {'FIID':'0808','NOMBRE':'PREPAGO                  ','NIT':'8300705271               '},
                          {'FIID':'0811','NOMBRE':'LEALTAD                  ','NIT':'8300705271               '},
                          {'FIID':'0812','NOMBRE':'COLSUBSIDIO              ','NIT':'8600073361               '},
                          {'FIID':'0814','NOMBRE':'COMFAMA                  ','NIT':'8909008419               '},
                          {'FIID':'0815','NOMBRE':'FESA ASSENDA             ','NIT':'8903211510               '},
                          {'FIID':'0816','NOMBRE':'FALABELLA                ','NIT':'9000479818               '},
                          {'FIID':'0817','NOMBRE':'JNTS                     '},
                          {'FIID':'0818','NOMBRE':'BANCAMOVIL               '},
                          {'FIID':'0819','NOMBRE':'COMFENALCO               '},
                          {'FIID':'0820','NOMBRE':'COOMEVA                  ','NIT':'9004061505               '},
                          {'FIID':'0821','NOMBRE':'TARJETA EXITO            ','NIT':'8909006089               '},
                          {'FIID':'0822','NOMBRE':'COMBARRANQUILLA          ','NIT':'8901020022               '},
                          {'FIID':'0823','NOMBRE':'COMFAMILIAR ATLANTICO    ','NIT':'8901019949               '},
                          {'FIID':'0824','NOMBRE':'SERVIBANCA COLSUBSIDIO   '},
                          {'FIID':'0825','NOMBRE':'COMFENALCO ANTIOQUIA     ','NIT':'8909008426               '},
                          {'FIID':'0826','NOMBRE':'MACROFINANCIERA - TEBCA  ','NIT':'9002708158               '},
                          {'FIID':'0827','NOMBRE':'MACROFINANCI - SERVITEBCA','NIT':'9002914804               '},
                          {'FIID':'0828','NOMBRE':'ASOPAGOS                 ','NIT':'9003192912               '},
                          {'FIID':'0829','NOMBRE':'DAVIPLATA CONEXRED       '},
                          {'FIID':'0831','NOMBRE':'RAYCO                    '},
                          {'FIID':'0902','NOMBRE':'SERVIBANCA               ','NIT':'8300366457               '},
                          {'FIID':'0903','NOMBRE':'ATH                      ','NIT':'8001434071               '},
                          {'FIID':'0906','NOMBRE':'COVINOC                  ','NIT':'8600284621               '},
                          {'FIID':'0907','NOMBRE':'FENALCHEQUE              '},
                          {'FIID':'0970','NOMBRE':'Fondo CNB-AVAL           '},
                          {'FIID':'0978','NOMBRE':'FONDO ADMIN PSP          '},
                          {'FIID':'0979','NOMBRE':'FONDO ADMIN PSP REMOTO   '},
                          {'FIID':'0983','NOMBRE':'RECARGA                  '},
                          {'FIID':'3205','NOMBRE':'ENTIDAD3205              '},
                          {'FIID':'3902','NOMBRE':'AMERICAN EXPRESS         '},
                          {'FIID':'7777','NOMBRE':'Donaciones               '},
                          {'FIID':'7778','NOMBRE':'Donaciones               '},
                          {'FIID':'9190','NOMBRE':'COLPATRIA - HOMCENTER      '},
                          {'FIID':'ASCR','NOMBRE':'CREDIBANCO               ','NIT':'8600329097               '},
                          {'FIID':'MDS ','NOMBRE':'MASTER DEBIT SWITCH      '},
                          {'FIID':'REDE','NOMBRE':'PRIVADAS\/ BONOS           ','NIT':'8300705271               '},
                          {'FIID':'0069','NOMBRE':'NUEVA ENTIDAD            ','NIT':'                         '},
                          {'FIID':'0068','NOMBRE':'NUEVA ENTIDAD            ','NIT':'                         '},
                          {'FIID':'0000','NOMBRE':'NUEVA ENTIDAD            ','NIT':'                         '}
                        ]";
            List<EntidadesModel> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EntidadesModel>>(json);
            return lst;
        }

        private List<FalabellaModel> UploadFalabella()
        {
            var json = @"[{'CODIGO_UNICO':'0012347498','LOCAL_FALABELLA':'0037','NOMBRE_LOCAL':'Santiago de Cali              '},
                          {'CODIGO_UNICO':'0011802790','LOCAL_FALABELLA':'0183','NOMBRE_LOCAL':'Santa Fe                      '},
                          {'CODIGO_UNICO':'0012347522','LOCAL_FALABELLA':'0232','NOMBRE_LOCAL':'Centro Comercial BIMA         '},
                          {'CODIGO_UNICO':'0012688644','LOCAL_FALABELLA':'0043','NOMBRE_LOCAL':'Santa fe Medellin             '},
                          {'CODIGO_UNICO':'0011893039','LOCAL_FALABELLA':'0108','NOMBRE_LOCAL':'Plaza Imperial                '},
                          {'CODIGO_UNICO':'0012347779','LOCAL_FALABELLA':'0005','NOMBRE_LOCAL':'Unicali                       '},
                          {'CODIGO_UNICO':'0012121216','LOCAL_FALABELLA':'0099','NOMBRE_LOCAL':'CLL 99                        '},
                          {'CODIGO_UNICO':'0012181384','LOCAL_FALABELLA':'0082','NOMBRE_LOCAL':'Hayuelos                      '},
                          {'CODIGO_UNICO':'0012347480','LOCAL_FALABELLA':'0123','NOMBRE_LOCAL':'Unicentro Bogota              '},
                          {'CODIGO_UNICO':'0012347506','LOCAL_FALABELLA':'0053','NOMBRE_LOCAL':'Galerias                      '},
                          {'CODIGO_UNICO':'0012661468','LOCAL_FALABELLA':'0038','NOMBRE_LOCAL':'CENTRO MAYOR                  '},
                          {'CODIGO_UNICO':'0012664785','LOCAL_FALABELLA':'2000','NOMBRE_LOCAL':'VENTA A DISTANCIA             '},
                          {'CODIGO_UNICO':'0012055885','LOCAL_FALABELLA':'0036','NOMBRE_LOCAL':'San Diego Medellin            '},
                          {'CODIGO_UNICO':'0012817441','LOCAL_FALABELLA':'0062','NOMBRE_LOCAL':'PARQUE ARBOLEDA PEREIRA       '}
                        ]";
            List<FalabellaModel> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FalabellaModel>>(json);
            return lst;
        }

        private List<CnbsModel> UploadCnbs()
        {
            var json = @"[{'CODIGO_UNICO':'3082000013','NOMBRE':'SUPERTIENDA TROPICANA                             '},
                          {'CODIGO_UNICO':'3082000039','NOMBRE':'SUPERMERCADO DONALDO                              '},
                          {'CODIGO_UNICO':'3082000120','NOMBRE':'SUPERMERCADO MERCADEFAM                           '},
                          {'CODIGO_UNICO':'3082000138','NOMBRE':'LOS BUCAROS SP                                    '},
                          {'CODIGO_UNICO':'3082000153','NOMBRE':'SUPERMERCADO MONOFRUVER                           '},
                          {'CODIGO_UNICO':'3082000179','NOMBRE':'PANADERIA DELIXPAN                                '},
                          {'CODIGO_UNICO':'3082000195','NOMBRE':'PANADERIA LA FLORIDA                              '},
                          {'CODIGO_UNICO':'3082100227','NOMBRE':'SUPERMERCADO LA ESPERANZA                         '},
                          {'CODIGO_UNICO':'3082100235','NOMBRE':'SUPERMERCADO MARANATHA                            '},
                          {'CODIGO_UNICO':'3082100250','NOMBRE':'GRANERO LAS BRISAS                                '},
                          {'CODIGO_UNICO':'3082100292','NOMBRE':'PANADERIA DOÑA MARIA                              '},
                          {'CODIGO_UNICO':'3082100300','NOMBRE':'TIENDA EL PORVENIR                                '},
                          {'CODIGO_UNICO':'3082100318','NOMBRE':'VIVERES JR                                        '},
                          {'CODIGO_UNICO':'3082100326','NOMBRE':'SUPERMERCADO MERCAFE                              '},
                          {'CODIGO_UNICO':'3082100334','NOMBRE':'HIPERAUTOSERVICIO EL GAITAN EXPRESS               '},
                          {'CODIGO_UNICO':'3082100342','NOMBRE':'AUTOSERVICIO COOPBARATIA                          '},
                          {'CODIGO_UNICO':'3082100383','NOMBRE':'MERCADOS_BLANCO_TORRES_LT                         '},
                          {'CODIGO_UNICO':'3082100409','NOMBRE':'HUGO_A_MEDINA_S                                   '},
                          {'CODIGO_UNICO':'3082100417','NOMBRE':'MERCADOS BETANCUR                                 '},
                          {'CODIGO_UNICO':'3082100425','NOMBRE':'DEPOSITO Y FERRETERIA PUERTO LOPEZ                '},
                          {'CODIGO_UNICO':'3082100433','NOMBRE':'AUTOSERVICIO TIERRABUENA                          '},
                          {'CODIGO_UNICO':'3082100458','NOMBRE':'EL GAITAN Y VILLAMARÍA EXPRES                     '},
                          {'CODIGO_UNICO':'3082100466','NOMBRE':'MERCADOS COPACABANA LTDA                          '},
                          {'CODIGO_UNICO':'3082200266','NOMBRE':'SALSAMENTARIA Y MINIMERCADO LA CIZALLA            '},
                          {'CODIGO_UNICO':'3082200282','NOMBRE':'EL GRAN PUNTO DEL GUARIN                          '},
                          {'CODIGO_UNICO':'3082300272','NOMBRE':'ESTANCO LOS DESCUENTOS                            '},
                          {'CODIGO_UNICO':'3180100053','NOMBRE':'SCALE PLOTTER                                     '},
                          {'CODIGO_UNICO':'3180100087','NOMBRE':'SAN PEDRO PLAZA NET                               '},
                          {'CODIGO_UNICO':'3180100137','NOMBRE':'MERCA SURTIVER                                    '},
                          {'CODIGO_UNICO':'3180100152','NOMBRE':'LAS MELO GURMET                                   '},
                          {'CODIGO_UNICO':'3180100160','NOMBRE':'LA GRAN PAPELERA                                  '},
                          {'CODIGO_UNICO':'3180100178','NOMBRE':'MERCATODO S.A                                     '},
                          {'CODIGO_UNICO':'3180100186','NOMBRE':'SUPERMERCADO LA CANASTA                           '},
                          {'CODIGO_UNICO':'3180100194','NOMBRE':'INCAP                                             '},
                          {'CODIGO_UNICO':'3180100202','NOMBRE':'COP MULTISERVICIOS COOTRACOLTA                    '},
                          {'CODIGO_UNICO':'3180100236','NOMBRE':'DULCERIA ESTRELLA DE LA 73                        '},
                          {'CODIGO_UNICO':'3180100244','NOMBRE':'PEOPLE INTERNET                                   '},
                          {'CODIGO_UNICO':'3180100269','NOMBRE':'SUPERTIENDA SU CASA                               '},
                          {'CODIGO_UNICO':'3180100277','NOMBRE':'S.TIENDA PRECIO FACIL                            '},
                          {'CODIGO_UNICO':'3180100285','NOMBRE':'KEFREN TIENDA                                     '},
                          {'CODIGO_UNICO':'3180100293','NOMBRE':'MARCASHOP MYN                                     '},
                          {'CODIGO_UNICO':'3180100301','NOMBRE':'COMERCIALIZADORA GIRALDO Y GARCIA                 '},
                          {'CODIGO_UNICO':'3180100319','NOMBRE':'SUPERMERCADO E Y M EL MANA                        '},
                          {'CODIGO_UNICO':'3180100335','NOMBRE':'MERCADOS LA ECONOMIA                              '},
                          {'CODIGO_UNICO':'3180100376','NOMBRE':'MERCADOS DELTA LTDA                               '},
                          {'CODIGO_UNICO':'3180100426','NOMBRE':'SUPERMERCADO AUTOSERVICIO GARFAM                  '},
                          {'CODIGO_UNICO':'3180100434','NOMBRE':'COORATIENDAS SANTANDER A&B                        '},
                          {'CODIGO_UNICO':'3180100442','NOMBRE':'KATY CHAT                                         '},
                          {'CODIGO_UNICO':'3180100459','NOMBRE':'TIENDA LA REBAJA                                  '},
                          {'CODIGO_UNICO':'3180100467','NOMBRE':'COMUNICACION TOTAL                                '},
                          {'CODIGO_UNICO':'3180100483','NOMBRE':'MEJORKOSTO TESCA NUEVA                            '},
                          {'CODIGO_UNICO':'3180100491','NOMBRE':'RAPI TIENDA LA MARIA                              '},
                          {'CODIGO_UNICO':'3180100509','NOMBRE':'MICROMERCADO KENNEDY                              '},
                          {'CODIGO_UNICO':'3180100517','NOMBRE':'PANES Y PASTELES EL DIAMANTE                      '},
                          {'CODIGO_UNICO':'3180100525','NOMBRE':'QUESERA Y SALSAMENTARIA ALTO JORDAN               '},
                          {'CODIGO_UNICO':'3180100533','NOMBRE':'EL BALCONCITO                                     '},
                          {'CODIGO_UNICO':'3180100541','NOMBRE':'GILBERTO ANTONIO BEDOYA VELEZ                     '},
                          {'CODIGO_UNICO':'3180100558','NOMBRE':'EL CENTAVO MENOS C                                '},
                          {'CODIGO_UNICO':'3180100566','NOMBRE':'INTERAPIDISIMO PUNTO LAS FLORE                    '},
                          {'CODIGO_UNICO':'3180102133','NOMBRE':'CENTRO DE COMUNICACIONES JCL                      '},
                          {'CODIGO_UNICO':'3180200069','NOMBRE':'DROGUERIA VIDA                                    '},
                          {'CODIGO_UNICO':'3180200077','NOMBRE':'DISTRI AVILA LTDA                                 '},
                          {'CODIGO_UNICO':'3180200119','NOMBRE':'SUPERTIENDA COMUNAL                               '},
                          {'CODIGO_UNICO':'3180300109','NOMBRE':'SUPERMERCADO PANORAMA                             '},
                          {'CODIGO_UNICO':'3180300125','NOMBRE':'OLIMPICA                                          '}
                        ]";

            List<CnbsModel> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CnbsModel>>(json);
            return lst;
        }

        private List<RedprivadasModel> Uploadredprivadas()
        {
            var json = @"[{'red':'0F','nombre':'CREDIBANCO CREDIUNO '},
                          {'red':'0G','nombre':'CREDIBANCO BIGPASS  '}
                         ]";
            List<RedprivadasModel> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RedprivadasModel>>(json);
            return lst;
        }

        private List<ConveniosModel> Uploadconvenios()
        {
            var json = @"[{'Id':'1','Nemo':'CONVSA1','diaini':'JU','Numdías':7,'Descripcion':'Comfama debito','emisor':'004 ','NIT':'8909008419','tiponegocio':'501','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':3},
                          {'Id':'2','Nemo':'CONVSA2','diaini':'LU','Numdías':7,'Descripcion':'Comfenalco Ctgena D','emisor':'005 ','NIT':'8904800237','tiponegocio':'502','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':2},
                          {'Id':'3','Nemo':'CONVSA3','diaini':'LU','Numdías':7,'Descripcion':'Combarranquilla D','emisor':'008 ','NIT':'8901020022','tiponegocio':'503','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':2},
                          {'Id':'4','Nemo':'CONVSA4','diaini':'LU','Numdías':7,'Descripcion':'Comfamar Debito','emisor':'017 ','NIT':'8903040338','tiponegocio':'504','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':1},
                          {'Id':'5','Nemo':'CONVSB1','diaini':'LU','Numdías':7,'Descripcion':'Comfama credito','emisor':'004 ','NIT':'8909008419','tiponegocio':'506','tipotrans':1,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SB','bolsillo':5},
                          {'Id':'6','Nemo':'CONVSB2','diaini':'LU','Numdías':7,'Descripcion':'Comfenalco Ctgena Cr','emisor':'005 ','NIT':'8904800237','tiponegocio':'507','tipotrans':1,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SB','bolsillo':3},
                          {'Id':'7','Nemo':'CONVSA5','diaini':'LU','Numdías':7,'Descripcion':'Comfacundi Debito','emisor':'018 ','NIT':'860045904','tiponegocio':'505','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':1},
                          {'Id':'8','Nemo':'CONVSB3','diaini':'LU','Numdías':7,'Descripcion':'Redeban Fondo Cr','emisor':'015 ','NIT':'8001735076','tiponegocio':'509','tipotrans':0,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SB','bolsillo':1},
                          {'Id':'9','Nemo':'CONVSA6','diaini':'LU','Numdías':7,'Descripcion':'Comfenalco Ant Db','emisor':'016 ','NIT':'8909008426','tiponegocio':'508','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':2},
                          {'Id':'10','Nemo':'CONVSA7','diaini':'LU','Numdías':7,'Descripcion':'Comfamiliar Atl Db','emisor':'009 ','NIT':'8901019949','tiponegocio':'511','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':2},
                          {'Id':'11','Nemo':'CONVSC2','diaini':'LU','Numdías':7,'Descripcion':'Comfenalco Ctgena BD','emisor':'005 ','NIT':'8904800237','tiponegocio':'564','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SC','bolsillo':8},
                          {'Id':'12','Nemo':'CONVSC7','diaini':'LU','Numdías':7,'Descripcion':'Comfamiliar Atl DS','emisor':'009 ','NIT':'8901019949','tiponegocio':'568','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SC','bolsillo':7},
                          {'Id':'13','Nemo':'CONVSF7','diaini':'LU','Numdías':7,'Descripcion':'Comfamiliar Atl BR','emisor':'009 ','NIT':'8901019949','tiponegocio':'569','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SF','bolsillo':6},
                          {'Id':'14','Nemo':'CONVSB7','diaini':'LU','Numdías':7,'Descripcion':'Comfamiliar Atl BE','emisor':'009 ','NIT':'8901019949','tiponegocio':'567','tipotrans':1,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SB','bolsillo':4},
                          {'Id':'15','Nemo':'CONVSC1','diaini':'JU','Numdías':7,'Descripcion':'Comfama debito CM','emisor':'004 ','NIT':'8909008419','tiponegocio':'510','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SC','bolsillo':2},
                          {'Id':'16','Nemo':'CONVSF1','diaini':'JU','Numdías':7,'Descripcion':'Comfama debito BN','emisor':'004 ','NIT':'8909008419','tiponegocio':'518','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SF','bolsillo':4},
                          {'Id':'17','Nemo':'CONVSA8','diaini':'LU','Numdías':7,'Descripcion':'Comfenalco tolima Db','emisor':'044 ','NIT':'8907001484','tiponegocio':'516','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':2},
                          {'Id':'18','Nemo':'CONVSA9','diaini':'LU','Numdías':7,'Descripcion':'Comfenalco Stder Db','emisor':'045 ','NIT':'8902015787','tiponegocio':'533','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':2},
                          {'Id':'19','Nemo':'CONVSA0','diaini':'LU','Numdías':7,'Descripcion':'Comfacesar','emisor':'046 ','NIT':'8002110251','tiponegocio':'543','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':2},
                          {'Id':'20','Nemo':'CONVSP3','diaini':'LU','Numdías':7,'Descripcion':'Comfaca ','emisor':'047 ','NIT':'00','tiponegocio':'571','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SP','bolsillo':1},
                          {'Id':'21','Nemo':'CONVSAB','diaini':'LU','Numdías':7,'Descripcion':'Comfacauca','emisor':'048 ','NIT':'9815001820','tiponegocio':'2039','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':1},
                          {'Id':'22','Nemo':'CONVSAC','diaini':'LU','Numdías':7,'Descripcion':'Comfamiliar Caldas','emisor':'050 ','NIT':'8908064905','tiponegocio':'2053','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':1},
                          {'Id':'23','Nemo':'CONVSP4','diaini':'LU','Numdías':7,'Descripcion':'Comfaguajira','emisor':'051 ','NIT':'00','tiponegocio':'951','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SP','bolsillo':1},
                          {'Id':'24','Nemo':'CONVSP2','diaini':'LU','Numdías':7,'Descripcion':'Comfacor','emisor':'052 ','NIT':'00','tiponegocio':'570','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SP','bolsillo':1},
                          {'Id':'25','Nemo':'CONVSP6','diaini':'LU','Numdías':7,'Descripcion':'Comfamiliar Huila','emisor':'053 ','NIT':'00','tiponegocio':'953','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SP','bolsillo':1},
                          {'Id':'26','Nemo':'CONVSAA','diaini':'LU','Numdías':7,'Descripcion':'Comfandi Muj Ahorr','emisor':'057 ','NIT':'8903032085','tiponegocio':'2054','tipotrans':2,'Servidor':'localhost','Usuario':'1','Clave':'','COD_CONCEPTO':'SA','bolsillo':1}
                        ]";
            List<ConveniosModel> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ConveniosModel>>(json);
            return lst;
        }

        private List<FestivoModel> Uploadfestivo()
        {
            var json = @"[{'FESTIVO':'20100104','DIAHABIL':'20100104'},
                          {'FESTIVO':'20100112','DIAHABIL':'20100112'},
                          {'FESTIVO':'20100118','DIAHABIL':'20100118'},
                          {'FESTIVO':'20100125','DIAHABIL':'20100125'},
                          {'FESTIVO':'20100201','DIAHABIL':'20100201'},
                          {'FESTIVO':'20100208','DIAHABIL':'20100208'},
                          {'FESTIVO':'20100215','DIAHABIL':'20100215'},
                          {'FESTIVO':'20100222','DIAHABIL':'20100222'},
                          {'FESTIVO':'20100301','DIAHABIL':'20100301'},
                          {'FESTIVO':'20100308','DIAHABIL':'20100308'},
                          {'FESTIVO':'20100315','DIAHABIL':'20100315'},
                          {'FESTIVO':'20100323','DIAHABIL':'20100323'},
                          {'FESTIVO':'20100329','DIAHABIL':'20100329'},
                          {'FESTIVO':'20100405','DIAHABIL':'20100405'},
                          {'FESTIVO':'20100412','DIAHABIL':'20100412'},
                          {'FESTIVO':'20100419','DIAHABIL':'20100419'},
                          {'FESTIVO':'20100426','DIAHABIL':'20100426'},
                          {'FESTIVO':'20100503','DIAHABIL':'20100503'},
                          {'FESTIVO':'20100510','DIAHABIL':'20100510'},
                          {'FESTIVO':'20100518','DIAHABIL':'20100518'},
                          {'FESTIVO':'20100524','DIAHABIL':'20100524'},
                          {'FESTIVO':'20100528','DIAHABIL':'20100528'},
                          {'FESTIVO':'20100531','DIAHABIL':'20100531'},
                          {'FESTIVO':'20100608','DIAHABIL':'20100608'},
                          {'FESTIVO':'20100615','DIAHABIL':'20100615'},
                          {'FESTIVO':'20100621','DIAHABIL':'20100621'},
                          {'FESTIVO':'20100706','DIAHABIL':'20100706'},
                          {'FESTIVO':'20100712','DIAHABIL':'20100712'},
                          {'FESTIVO':'20100719','DIAHABIL':'20100719'},
                          {'FESTIVO':'20100721','DIAHABIL':'20100721'},
                          {'FESTIVO':'20100726','DIAHABIL':'20100726'},
                          {'FESTIVO':'20100802','DIAHABIL':'20100802'},
                          {'FESTIVO':'20100809','DIAHABIL':'20100809'},
                          {'FESTIVO':'20100811','DIAHABIL':'20100811'},
                          {'FESTIVO':'20100817','DIAHABIL':'20100817'},
                          {'FESTIVO':'20100823','DIAHABIL':'20100823'},
                          {'FESTIVO':'20100830','DIAHABIL':'20100830'},
                          {'FESTIVO':'20100906','DIAHABIL':'20100906'},
                          {'FESTIVO':'20100913','DIAHABIL':'20100913'},
                          {'FESTIVO':'20100920','DIAHABIL':'20100920'},
                          {'FESTIVO':'20100927','DIAHABIL':'20100927'},
                          {'FESTIVO':'20101004','DIAHABIL':'20101004'},
                          {'FESTIVO':'20101019','DIAHABIL':'20101019'},
                          {'FESTIVO':'20101025','DIAHABIL':'20101025'},
                          {'FESTIVO':'20101102','DIAHABIL':'20101102'},
                          {'FESTIVO':'20101108','DIAHABIL':'20101108'},
                          {'FESTIVO':'20101116','DIAHABIL':'20101116'},
                          {'FESTIVO':'20101122','DIAHABIL':'20101122'},
                          {'FESTIVO':'20101129','DIAHABIL':'20101129'},
                          {'FESTIVO':'20101206','DIAHABIL':'20101206'},
                          {'FESTIVO':'20101209','DIAHABIL':'20101209'},
                          {'FESTIVO':'20101213','DIAHABIL':'20101213'},
                          {'FESTIVO':'20101220','DIAHABIL':'20101220'},
                          {'FESTIVO':'20101227','DIAHABIL':'20101227'}
                        ]";
            List<FestivoModel> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FestivoModel>>(json);
            return lst;
        }
    }
}

