import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.InputStreamReader;
import java.io.IOException;
import java.io.InputStream;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Arrays;
import java.util.Properties;

class MySQL{
    private Connection connection;
    private Statement statement;
    private PreparedStatement preparedStatement;
    private String host;
    private String port;
    private String database;
    private String username;
    private String password;
    private String url;

    public MySQL(String propertyPath) throws Exception{
        Properties config = new Properties();
        InputStream input = new FileInputStream(propertyPath); 
        config.load(input);
        host = config.getProperty("host");
        port = config.getProperty("port");
        database = config.getProperty("database");
        username = config.getProperty("username");
        password = config.getProperty("password");
        input.close();
        url = "jdbc:mysql://" + host + ":" + port + "/" + database + "?autoReconnect=true&useSSL=false";
        Class.forName("com.mysql.jdbc.Driver").newInstance();
    }

    public ResultSet select(String query) throws SQLException{
        connection = DriverManager.getConnection(url, username, password);  
        statement = connection.createStatement();
        ResultSet resultSet = statement.executeQuery(query);
        return resultSet;
    }

}