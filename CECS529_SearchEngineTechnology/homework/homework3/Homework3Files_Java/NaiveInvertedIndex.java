
import java.util.*;

public class NaiveInvertedIndex {
   private HashMap<String, List<Integer>> mIndex;
   
   public NaiveInvertedIndex() {
      mIndex = new HashMap<String, List<Integer>>();
   }
   
   public void addTerm(String term, int documentID) {
      // TO-DO: add the term to the index hashtable. If the table does not have
      // an entry for the term, initialize a new ArrayList<Integer>, add the 
      // docID to the list, and put it into the map. Otherwise add the docID
      // to the list that already exists in the map, but ONLY IF the list does
      // not already contain the docID.
      
      
      
   }
   
   public List<Integer> getPostings(String term) {
      // TO-DO: return the postings list for the given term from the index map.
      
      return null;
   }
   
   public int getTermCount() {
      // TO-DO: return the number of terms in the index.
      
      return 0;
   }
   
   public String[] getDictionary() {
      // TO-DO: fill an array of Strings with all the keys from the hashtable.
      // Sort the array and return it.
      
      return null;
   }
}
