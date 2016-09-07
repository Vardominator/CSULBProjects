# read arguments
args <- commandArgs(TRUE)


print(paste(args, collapse = ' ', sep = " "))

options(java.parameters = "- Xmx1024m")

library(rJava)
library(NLP)
library(openNLP)

text <- as.String(paste(args, collapse = ' ', sep = " "));

word_ann <- Maxent_Word_Token_Annotator();
sent_ann <- Maxent_Sent_Token_Annotator();
pos_ann <- Maxent_POS_Tag_Annotator();

pos_annotations <- annotate(text, list(sent_ann, word_ann, pos_ann));
text_annotations <- annotate(text, list(sent_ann, word_ann));

text_doc <- AnnotatedPlainTextDocument(text, text_annotations);

person_ann <- Maxent_Entity_Annotator(kind = "person");
location_ann <- Maxent_Entity_Annotator(kind = "location");
organization_ann <- Maxent_Entity_Annotator(kind = "organization");
date_ann <- Maxent_Entity_Annotator(kind = "date");

pipeline <- list(sent_ann,
                 word_ann,
                 person_ann,
                 location_ann,
                 organization_ann,
                 date_ann);

text_annotations <- annotate(text, pipeline);
text_doc <- AnnotatedPlainTextDocument(text, text_annotations);

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


print(entities(text_doc, kind = "person"));
print(entities(text_doc, kind = "location"));
print(entities(text_doc, kind = "organization"));
print(entities(text_doc, kind = "date"));



