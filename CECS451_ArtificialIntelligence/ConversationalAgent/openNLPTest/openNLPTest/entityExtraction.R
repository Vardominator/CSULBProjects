require("NLP")
require("openNLP")
require("rJava")


text <- "I would like to travel to Chicago on January 7th, 2017.";
print(text);

text <- paste(text, collapse = " ");
print(text);

text <- as.String(text);

word_ann <- Maxent_Word_Token_Annotator();
sent_ann <- Maxent_Sent_Token_Annotator();
pos_ann <- Maxent_POS_Tag_Annotator();

pos_annotations <- annotate(text, list(sent_ann, word_ann, pos_ann));
text_annotations <- annotate(text, list(sent_ann, word_ann));
print(head(text_annotations));


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

