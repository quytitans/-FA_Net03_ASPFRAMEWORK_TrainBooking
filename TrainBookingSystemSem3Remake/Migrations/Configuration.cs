namespace TrainBookingSystemSem3Remake.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TrainBookingSystemSem3Remake.Data;
    using TrainBookingSystemSem3Remake.Enum;
    using TrainBookingSystemSem3Remake.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TrainBookingSystemSem3Remake.Data.TrainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TrainBookingSystemSem3Remake.Data.TrainContext context)
        {
            var trainContext = new TrainContext();
            //  This method will be called after migrating to the latest version.

            var trainStaition = new List<TrainStation>{
                new TrainStation{Id = 1, Name="Hà Nội", DetailAddress = "Số 120, phố Lê Duẩn, phường Cửa Nam, quận Hoàn Kiếm, Hà Nội", City="Hà Nội",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 2, Name="Giáp Bát", DetailAddress = "	Số 366, đường Giải Phóng, phường Giáp Bát, quận Hoàng Mai, Hà Nội", City="Hà Nội",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 3, Name="Đồng Văn", DetailAddress = "Quốc lộ 1, phường Đồng Văn, thị xã Duy Tiên, Hà Nam", City="Phủ Lý",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 4, Name="Nam Định", DetailAddress = "	Số 2, đường Trần Đăng Ninh, thành phố Nam Định, Nam Định", City="Nam Định",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 5, Name="Sài Gòn", DetailAddress = "	1 Nguyễn Thông, phường 9, quận 3, Thành phố Hồ Chí Minh", City="Sài Gòn",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 6, Name="Long Khánh", DetailAddress = "Số 23, đường Trần Phú, phường Xuân An, thành phố Long Khánh, Đồng Nai", City="Long Khánh",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 7, Name="Tháp Chàm", DetailAddress = "Đường Minh Mạng, phường Đô Vinh, thành phố Phan Rang – Tháp Chàm, Ninh Thuận", City="Phan Rang – Tháp Chàm",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 8, Name="Bình Thuận", DetailAddress = "Xã Mương Mán, huyện Hàm Thuận Nam, Bình Thuận", City="Phan Thiết",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 9, Name="Nha Trang", DetailAddress = "17 Thái Nguyên, phường Phước Tân, thành phố Nha Trang, Khánh Hòa", City="Nha Trang",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 10, Name="Đà Nẵng", DetailAddress = "Số 791, đường Hải Phòng, phường Tam Thuận, quận Thanh Khê, Đà Nẵng", City="Đà Nẵng",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 11, Name="Huế", DetailAddress = "Số 2, đường Bùi Thị Xuân, phường An Đông, thành phố Huế, Thừa Thiên Huế", City="Huế",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 12, Name="Quảng Ngãi", DetailAddress = "	Số 001, đường Nguyễn Chánh, phường Trần Phú, thành phố Quảng Ngãi, Quảng Ngãi", City="Quãng Ngãi",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 13, Name="Mỹ Đức", DetailAddress = "Thôn Mỹ Đức, xã Sơn Thủy, huyện Lệ Thủy, Quảng Bình", City="Đồng Hới",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 14, Name="Vinh", DetailAddress = "Số 1, đường Lê Ninh, phường Quán Bàu, thành phố Vinh, Nghệ An", City="Vinh",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 15, Name="Hải Phòng", DetailAddress = "75 Lương Khánh Thiện, Ngô Quyền - Hải Phòng", City="Hải Phòng",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 16, Name="Hải Dương", DetailAddress = "Hồng Quang - TP. Hải Dương", City="Hải Dương",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 17, Name="Lào Cai", DetailAddress = "Phường Phố Mới,thị xã Lào Cai, tỉnh Lào Cai", City="Lào Cai",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 18, Name="Đồng Đăng", DetailAddress = "Thị trấn Đồng Đăng, huyện Cao Lộc, tỉnh Lạng Sơn", City="Lạng Sơn",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 19, Name="Gò Vấp", DetailAddress = "Số 1, đường Lê Lai, phường 3, quận Gò Vấp, Thành phố Hồ Chí Minh", City="Sài Gòn",Status=(int)TrainStationEnum.ACTIVE},
                new TrainStation{Id = 20, Name="Tuy Hòa", DetailAddress = "Số 149, đường Lê Trung Kiên, phường 2, thành phố Tuy Hòa, Phú Yên", City="Tuy Hòa",Status=(int)TrainStationEnum.ACTIVE}
            };
            trainStaition.ForEach(s => trainContext.TrainStations.Add(s));
            trainContext.SaveChanges();
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var trainTypes = new List<TrainType>
            {
                new TrainType {Id = 1, Name = "Tàu Thống Nhất"},
                new TrainType {Id = 2, Name = "Tàu Viet Sun"},
                new TrainType {Id = 3, Name = "Tàu Biển Đông"},
                new TrainType {Id = 4, Name = "Tàu Vinafco"},
            };
            trainTypes.ForEach(s => trainContext.TrainTypes.Add(s));
            trainContext.SaveChanges();

            var trains = new List<Train>
            {
                new Train {Id = 1, TrainTypeId = 1, Name = "TNL001", Status = 1},
                new Train {Id = 2, TrainTypeId = 1, Name = "TNL002", Status = 1},
                new Train {Id = 3, TrainTypeId = 2, Name = "VNL003", Status = 1},
                new Train {Id = 4, TrainTypeId = 2, Name = "VNL004", Status = 1},
                new Train {Id = 5, TrainTypeId = 3, Name = "DNL005", Status = 1},
                new Train {Id = 6, TrainTypeId = 3, Name = "DNS001", Status = 1},
                new Train {Id = 7, TrainTypeId = 4, Name = "CNS002", Status = 1},
                new Train {Id = 8, TrainTypeId = 4, Name = "CNS003", Status = 1},
                new Train {Id = 9, TrainTypeId = 1, Name = "TNS004", Status = 1},
                new Train {Id = 10, TrainTypeId = 2, Name = "VNS005", Status = 1},
            };
            trains.ForEach(s => trainContext.Trains.Add(s));
            trainContext.SaveChanges();

            var trainCarriages = new List<TrainCarriages>
            {
                new TrainCarriages{Name= "Toa 1", NumRow = 12, NumCol = 4, TrainId = 1},
                new TrainCarriages{Name= "Toa 2", NumRow = 12, NumCol = 4, TrainId = 1},
                new TrainCarriages{Name= "Toa 3", NumRow = 12, NumCol = 4, TrainId = 1},
                new TrainCarriages{Name= "Toa 4", NumRow = 12, NumCol = 4, TrainId = 1},
                new TrainCarriages{Name= "Toa 1", NumRow = 12, NumCol = 4, TrainId = 2},
                new TrainCarriages{Name= "Toa 2", NumRow = 12, NumCol = 4, TrainId = 2},
                new TrainCarriages{Name= "Toa 3", NumRow = 12, NumCol = 4, TrainId = 2},
                new TrainCarriages{Name= "Toa 4", NumRow = 12, NumCol = 4, TrainId = 2},
                new TrainCarriages{Name= "Toa 1", NumRow = 12, NumCol = 4, TrainId = 3},
                new TrainCarriages{Name= "Toa 2", NumRow = 12, NumCol = 4, TrainId = 3},
                new TrainCarriages{Name= "Toa 3", NumRow = 12, NumCol = 4, TrainId = 3},
                new TrainCarriages{Name= "Toa 4", NumRow = 12, NumCol = 4, TrainId = 3},
                new TrainCarriages{Name= "Toa 1", NumRow = 12, NumCol = 4, TrainId = 4},
                new TrainCarriages{Name= "Toa 2", NumRow = 12, NumCol = 4, TrainId = 4},
                new TrainCarriages{Name= "Toa 3", NumRow = 12, NumCol = 4, TrainId = 4},
                new TrainCarriages{Name= "Toa 4", NumRow = 12, NumCol = 4, TrainId = 4},
                new TrainCarriages{Name= "Toa 1", NumRow = 12, NumCol = 4, TrainId = 5},
                new TrainCarriages{Name= "Toa 2", NumRow = 12, NumCol = 4, TrainId = 5},
                new TrainCarriages{Name= "Toa 3", NumRow = 12, NumCol = 4, TrainId = 5},
                new TrainCarriages{Name= "Toa 4", NumRow = 12, NumCol = 4, TrainId = 5},
                new TrainCarriages{Name= "Toa 1", NumRow = 12, NumCol = 4, TrainId = 6},
                new TrainCarriages{Name= "Toa 2", NumRow = 12, NumCol = 4, TrainId = 6},
                new TrainCarriages{Name= "Toa 1", NumRow = 12, NumCol = 4, TrainId = 7},
                new TrainCarriages{Name= "Toa 2", NumRow = 12, NumCol = 4, TrainId = 7},
                new TrainCarriages{Name= "Toa 1", NumRow = 12, NumCol = 4, TrainId = 8},
                new TrainCarriages{Name= "Toa 2", NumRow = 12, NumCol = 4, TrainId = 8},
                new TrainCarriages{Name= "Toa 1", NumRow = 12, NumCol = 4, TrainId = 9},
                new TrainCarriages{Name= "Toa 2", NumRow = 12, NumCol = 4, TrainId = 9},
                new TrainCarriages{Name= "Toa 1", NumRow = 12, NumCol = 4, TrainId = 10},
                new TrainCarriages{Name= "Toa 2", NumRow = 12, NumCol = 4, TrainId = 10},
            };
            trainCarriages.ForEach(s => trainContext.TrainCarriages.Add(s));
            trainContext.SaveChanges();
        }
    }
}
