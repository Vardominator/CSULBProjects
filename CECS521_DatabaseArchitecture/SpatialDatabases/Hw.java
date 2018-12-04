import java.sql.ResultSet;;

class Hw {
    public static void main(String[] args){
        MySQL mysql = null;
        try {
            mysql = new MySQL(args[0]);
        } catch (Exception exception){
            exception.printStackTrace();
        }

        switch (args[1]){
            case "q1":
                try {
                    StringBuilder builder = new StringBuilder();
                    for(int i = 3; i < args.length - 1; i += 2){
                        builder.append(args[i] + " " + args[i + 1] + ",");
                    }
                    builder.append(args[3] + " " + args[4]);
                    String zone = builder.toString();

                    String query = "SELECT IncidentID,X(Location),Y(Location),Type FROM Incidents ";
                    query += "WHERE ST_Contains(PolygonFromText('POLYGON((" + zone + "))'), Location) ";
                    query += "ORDER BY IncidentID;";
                    
                    ResultSet resultSet = mysql.select(query);
                    while(resultSet.next()){
                        System.out.println(resultSet.getString(1) + "  " + resultSet.getString(2) + "," + resultSet.getString(3) + "  " + resultSet.getString(4).replace("\"", ""));
                    }

                } catch (Exception ex){
                    ex.printStackTrace();
                }
                break;

            case "q2":
                try {
                    String incidentID = args[2];
                    String distance = args[3];

                    String query = "SELECT DISTINCT O.BadgeNumber, ST_Distance_Sphere(I.Location, O.Location), O.OfficerName ";
                    query += "FROM Incidents AS I, Officers AS O ";
                    query += "WHERE I.IncidentID = " + incidentID + " AND ST_Distance_Sphere(I.Location, O.Location) <= " +  distance;
                    query += " ORDER BY ST_Distance_Sphere(I.Location, O.Location);";

                    ResultSet resultSet = mysql.select(query);
                    while(resultSet.next()){
                        System.out.println(resultSet.getString(1) + "  " + Math.round(Float.parseFloat(resultSet.getString(2))) + "m  " + resultSet.getString(3).replace("\"", ""));
                    }

                } catch (Exception ex){
                    ex.printStackTrace();
                }
                break;

            case "q3":
                try{
                    String squadNumber = args[2];

                    String query1 = "SELECT DISTINCT SquadNumber, ZoneName ";
                    query1 += " FROM Zones WHERE SquadNumber = " + squadNumber;
                    
                    ResultSet resultSet = mysql.select(query1);
                    while(resultSet.next()){
                        System.out.println("Squad " + resultSet.getString(1) + " is now patrolling: " + resultSet.getString(2));
                    }

                    
                    String query2 = "SELECT O.BadgeNumber, ST_Contains(Z.Region, O.Location), O.OfficerName ";
                    query2 += "FROM Zones Z, Officers O ";
                    query2 += "WHERE Z.SquadNumber = O.SquadNumber AND Z.SquadNumber = " + squadNumber;
                    query2 += " ORDER BY O.BadgeNumber";

                    resultSet = mysql.select(query2);
                    while(resultSet.next()){
                        String officerIn = (resultSet.getString(2).equals("1")) ? "YES":"NO";
                        System.out.println(resultSet.getString(1) + "  " + officerIn + "  " + resultSet.getString(3));
                    }

                } catch (Exception ex){
                    ex.printStackTrace();
                }

                break;

            case "q4":
                try {
                    String routeNumber = args[2];

                    String query = "SELECT Z.ZoneID, ST_Crosses(Z.Region, R.Route), Z.ZoneName ";
                    query += "FROM Zones Z, Routes R ";
                    query += "WHERE Crosses(R.Route, Z.Region)";

                    ResultSet resultSet = mysql.select(query);
                    while(resultSet.next()){
                        System.out.println(resultSet.getString(2));
                    }

                } catch (Exception ex) {
                    ex.printStackTrace();
                } 

                break;

        }
        
    }


    public static float geographicaDistance(String metersStr){
        float meters = Float.parseFloat(metersStr);
        float earthRadius = 6371e3f; // in meters

        return meters / earthRadius;
    }

}