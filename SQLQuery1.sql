select distinct h.* from (
	select r.Id, r.HotelId, r.Availability, count(b.Id) book
			from Rooms r left join Bookings b on r.Id=b.RoomId
			group by r.Id, r.HotelId, r.Availability) avail
	join Hotels h on avail.HotelId = h.Id