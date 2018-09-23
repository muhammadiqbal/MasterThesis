
from sklearn.externals import joblib
model = joblib.load('model_emailLines_class_uncleaned.pkl')
emailLine = input()
print(model.predict([emailLine])[0])