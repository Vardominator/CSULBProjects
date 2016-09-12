# Uses the 'openNLP' library to perform natural language processing
#   on an input text.  The 'openNLP' library requires the 'rJava' and
#   'NLP' libraries to execute all of the necessary functions.
# 
# Args: 
#   x: Any string of characters
#  
# Returns: 
#    A list of persons, locations, organizations, and dates within that text.
#


# read arguments and initialize java access
args <- commandArgs(TRUE)
options(java.parameters = "- Xmx1024m")

# load necessary libraries
library(rJava)
library(NLP)
library(openNLP)

# combine all input arguments into a single string
text <- as.String(paste(args, collapse = ' ', sep = " "));

# set up annotator methods
word_ann <- Maxent_Word_Token_Annotator();
sent_ann <- Maxent_Sent_Token_Annotator();
pos_ann <- Maxent_POS_Tag_Annotator();

# perform annotations
pos_annotations <- annotate(text, list(sent_ann, word_ann, pos_ann));
text_annotations <- annotate(text, list(sent_ann, word_ann));

# set up methods for finding entitities within the annotations
person_ann <- Maxent_Entity_Annotator(kind = "person");
location_ann <- Maxent_Entity_Annotator(kind = "location");
organization_ann <- Maxent_Entity_Annotator(kind = "organization");
date_ann <- Maxent_Entity_Annotator(kind = "date");

# combine all extraction methods into a single pipeline
pipeline <- list(sent_ann,
                 word_ann,
                 person_ann,
                 location_ann,
                 organization_ann,
                 date_ann);


# run methods on input text
text_annotations <- annotate(text, pipeline);
text_doc <- AnnotatedPlainTextDocument(text, text_annotations);

# use this method to extract the lists of entities
# verbose: if you pass in the category it will return only entities
#   from that category
entities <- function(doc, kind) {
    s <- doc$content;
    a <- annotations(doc)[[1]]
    if (hasArg(kind)) {
        k <- sapply(a$features, '[[', "kind")
        s[a[k == kind]]
    } else {
        s[a[a$type == "entity"]];
    }
}

# print the results into the stream
print(entities(text_doc, kind = "person"));
print(entities(text_doc, kind = "location"));
print(entities(text_doc, kind = "organization"));
print(entities(text_doc, kind = "date"));



