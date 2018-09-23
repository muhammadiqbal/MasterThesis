import pandas as pd
import numpy as np
from sklearn.pipeline import Pipeline
import re 
from sklearn.model_selection import train_test_split
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.feature_extraction.text import TfidfTransformer
from sklearn.multiclass import OneVsRestClassifier
from sklearn.svm import LinearSVC
#from sklearn.grid_search import GridSearchCV
from sklearn.model_selection import GridSearchCV

#load data
df = pd.read_csv("emailLines.csv")

#pre-processing
def clean_str(string):
    """
    Tokenization/string cleaning for dataset
    Every dataset is lower cased except
    """
    string = re.sub(r"\n", "", string)    
    string = re.sub(r"\r", "", string) 
    string = re.sub(r"[0-9]", "digit", string)
    string = re.sub(r"\'", "", string)    
    string = re.sub(r"\"", "", string)    
    return string.strip().lower()
X = []
for i in range(df.shape[0]):
    #X.append(clean_str(df.iloc[i][1]))
    X.append(df.iloc[i][1])
y = np.array(df["class"])

#train test split
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.01, random_state=5)

#pipeline of feature engineering and model
#preparing the final pipeline using the selected parameters
model = Pipeline([('vectorizer', CountVectorizer(ngram_range=(1,2))),
    ('tfidf', TfidfTransformer(use_idf=True)),
    ('clf', OneVsRestClassifier(LinearSVC(class_weight="balanced")))])
#model = Pipeline([(‘vectorizer’, CountVectorizer()),(‘tfidf’, TfidfTransformer()),(‘clf’, OneVsRestClassifier(LinearSVC(class_weight='balanced')))])
#the class_weight="balanced" option tries to remove the biasedness of model towards majority sample

#paramater selection

parameters = {'vectorizer__ngram_range': [(2, 2),(2, 3)],
               'tfidf__use_idf': (True, False)}
gs_clf_svm = GridSearchCV(model, parameters, n_jobs=-1)
gs_clf_svm = gs_clf_svm.fit(X, y)
print(gs_clf_svm.best_score_)
print(gs_clf_svm.best_params_)

#preparing the final pipeline using the selected parameters
model = Pipeline([('vectorizer', CountVectorizer(ngram_range=(1,2))),
    ('tfidf', TfidfTransformer(use_idf=True)),
    ('clf', OneVsRestClassifier(LinearSVC(class_weight="balanced")))])

#fit model with training data
model.fit(X_train, y_train)
#evaluation on test data
pred = model.predict(X_test)
from sklearn.metrics import confusion_matrix, accuracy_score
confusion_matrix(pred, y_test)

#model.fit(X, y)

from sklearn.externals import joblib
model = joblib.dump(model,'model_emailLines_class_uncleaned.pkl')