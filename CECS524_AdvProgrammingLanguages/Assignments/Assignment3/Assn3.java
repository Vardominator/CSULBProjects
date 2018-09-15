import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;

class Assn3{
    public static void main(String[] args){
        String hexDecPattern = "0[xX](([0-9a-fA-F])*)(\\.([0-9a-fA-F])*)?(p|P)(\\+|-|$)(\\d\\d*)((f|F)|(l|L)|$)";
        try {
            File file = new File(args[0]);
            FileReader fileReader = new FileReader(file);
            BufferedReader bufferedReader = new BufferedReader(fileReader);
            String line;
            while((line = bufferedReader.readLine()) != null){
                boolean matches = line.matches(hexDecPattern);
                if(matches){
                    System.out.println("Matched: " + line);
                } else{
                    System.out.println("Not Matched: " + line);
                }
            }
        } catch (IOException e){
            e.printStackTrace();
        }
    }
}