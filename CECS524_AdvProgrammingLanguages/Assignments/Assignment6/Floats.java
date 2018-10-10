import java.io.IOException;
import java.nio.charset.Charset;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

class Floats {
    public static void main(String[] args){
        /*
        1. CONVERT IEEE 754 MAXIMUM AND MINIMUM FLOATING POINT HEXADECIMAL VALUES TO
        BITS THEN TO THEIR RESPECTIVE FLOATING POINT REPRESENTATIONS

        2. DIVIDE THE MAXIMUM VALUE BY 100 THEN MULTIPLY BY 2 TO GET 100 EQUAL INTERVALS
        BETWEEN THE MAXIMUM AND THE MINIMUM

        3. START FROM THE MINIMUM VALUE AND ADD THE INTERVAL UNTIL REACHING THE MAXIMUM

        4. USING THE FLOAT TO INT BIT MAPPING FUNCTION, FIND THE INTEGER DIFFERENCE BETWEEN
        EACH PAIR OF BOUNDARIES. THIS WILL GIVE THE NUMBER OF FLOATING POINT VALUES
        WITHIN EACH PAIR OF BOUNDARIES.

        5. PRINT THE RESULTS TO A FILE
        */
        Long floatMinHex = Long.parseLong("FF7FFFFF", 16);
        Long floatMaxHex = Long.parseLong("7F7FFFFF", 16);
        Float floatMin = Float.intBitsToFloat(floatMinHex.intValue());
        Float floatMax = Float.intBitsToFloat(floatMaxHex.intValue());
        float bucketSplit = 100f;
        float change = (floatMax / bucketSplit) * 2;
        float start = floatMin;
 
        List<String> lines = new ArrayList<String>();
        while(start < floatMax - change){
            float left = start;
            float right = start + change;
            int numFloats = Math.abs(Float.floatToIntBits(right) - Float.floatToIntBits(left));         
            lines.add(left + "," + right + "," + numFloats);
            start += change;
        }
        Path file = Paths.get("float_ranges.txt");
        try{
            Files.write(file, lines, Charset.forName("UTF-8"));
        } catch (IOException ex){
            
        }
    }
}