import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.InputStreamReader;
import java.io.IOException;
import java.io.InputStream;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Arrays;
import java.util.Properties;


public class Populate {
    static String zonePath = "";
    static String officerPath = "";
    static String routePath = "";
    static String incidentPath = "";
    static Connection conn = null;
    static Statement statement = null;
    static PreparedStatement preparedStatement = null;

    public static void main(String[] args) throws Exception {
        // SET FILE PATHS
        zonePath = args[1];
        officerPath = args[2];
        routePath = args[3];
        incidentPath = args[4];

        // LOAD CONFIGURATION
        Properties config = new Properties();
        InputStream input = null;
        input = new FileInputStream(args[0]);
        config.load(input);
        String host = config.getProperty("host");
        String port = config.getProperty("port");
        String database = config.getProperty("database");
        String username = config.getProperty("username");
        String password = config.getProperty("password");
        input.close();

        String url = "jdbc:mysql://" + host + ":" + port + "/" + database + "?autoReconnect=true&useSSL=false";

        try {
            Class.forName("com.mysql.jdbc.Driver").newInstance();
            conn = DriverManager.getConnection(url, username, password);
        } catch (SQLException exception){
            exception.printStackTrace();
        }
        

        // POPULATE ZONES
        FileInputStream fileStream = new FileInputStream(zonePath);
        BufferedReader br = new BufferedReader(new InputStreamReader(fileStream));
        String line;

        while ((line = br.readLine()) != null) {
            String[] inputs = line.trim().split("\\s*,\\s*");
            String zoneID = inputs[0];
            String zoneName = inputs[1];
            String squadNumber = inputs[2];
            StringBuilder builder = new StringBuilder("(");

            for (int i = 4; i < inputs.length - 1; i += 2){
                builder.append(inputs[i] + " " + inputs[i + 1] + ",");
            }

            builder.append(inputs[4] + " " + inputs[5]);
            builder.append(")");

            String zone = builder.toString();
            preparedStatement = conn.prepareStatement("SET @g = 'POLYGON(" + zone + ")'");
            preparedStatement.executeUpdate();

            try{
                preparedStatement = conn.prepareStatement("INSERT INTO PublicSafety.Zones VALUES (?, ?, ?, PolygonFromText(@g))");
                preparedStatement.setString(1, zoneID);
                preparedStatement.setString(2, zoneName);
                preparedStatement.setString(3, squadNumber);
                preparedStatement.executeUpdate();
            } catch (SQLException exception){
                exception.printStackTrace();
            }
            
        }
        
        br.close();


        // POPULATE OFFICERS
        fileStream = new FileInputStream(officerPath);
        br = new BufferedReader(new InputStreamReader(fileStream));
        line = null;
        while ((line = br.readLine()) != null) {
            String[] inputs = line.trim().split("\\s*,\\s*");
            String badge = inputs[0];
            String name = inputs[1];
            String squadNumber = inputs[2];
            
            try {
                preparedStatement = conn.prepareStatement("INSERT INTO PublicSafety.Officers VALUES (?, ?, ?, POINT(?, ?))");
                preparedStatement.setString(1, badge);
                preparedStatement.setString(2, name);
                preparedStatement.setString(3, squadNumber);
                preparedStatement.setString(4, inputs[3]);
                preparedStatement.setString(5, inputs[4]);
                preparedStatement.executeUpdate();
            } catch (SQLException exception){
                exception.printStackTrace();
            }
        }

        br.close();


        // POPULATE ROUTES
        fileStream = new FileInputStream(routePath);
        br = new BufferedReader(new InputStreamReader(fileStream));
        line = null;
        while ((line = br.readLine()) != null) {
            String[] inputs = line.trim().split("\\s*,\\s*");
            String routeNum = inputs[0];
            StringBuilder builder = new StringBuilder();

            for (int i = 3; i < inputs.length - 1; i += 2){
                builder.append(inputs[i] + " " + inputs[i + 1] + ",");
            }
            builder.deleteCharAt(builder.length() - 1);

            String route = builder.toString();
            preparedStatement = conn.prepareStatement("SET @g = 'LINESTRING(" + route + ")'");
            preparedStatement.executeUpdate();

            try{
                preparedStatement = conn.prepareStatement("INSERT INTO PublicSafety.Routes VALUES (?, GeomFromText(@g))");
                preparedStatement.setString(1, routeNum);
                preparedStatement.executeUpdate();
            } catch (SQLException exception){
                exception.printStackTrace();
            }

        }

        br.close();


        // POPULATE ROUTES
        fileStream = new FileInputStream(incidentPath);
        br = new BufferedReader(new InputStreamReader(fileStream));
        line = null;
        while ((line = br.readLine()) != null) {
            String[] inputs = line.trim().split("\\s*,\\s*");
            String incidentID = inputs[0];
            String name = inputs[1];

            try {
                preparedStatement = conn.prepareStatement("INSERT INTO PublicSafety.Incidents VALUES (?, ?, POINT(?, ?))");
                preparedStatement.setString(1, incidentID);
                preparedStatement.setString(2, name);
                preparedStatement.setString(3, inputs[2]);
                preparedStatement.setString(4, inputs[3]);
                preparedStatement.executeUpdate();
            } catch (SQLException exception){
                exception.printStackTrace();
            }
        }

        br.close();

    }

} 