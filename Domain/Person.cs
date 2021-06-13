using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Person:BaseEntity
    {
        public string PrivateNumber { get; set; }
        public string FirstnameGE { get; set; }
        public string FirstnameEN { get; set; }
        public string LastnameGE { get; set; }
        public string LastnameEN { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        private readonly List<RelatedPerson> _relatedPeople = new List<RelatedPerson>();
        public ICollection<RelatedPerson> RelatedPeople => _relatedPeople;

        private readonly List<Photo> _photos = new List<Photo>();
        public IReadOnlyList<Photo> Photos => _photos.AsReadOnly();



        public void AddRelatedPerson(Guid personId, Guid relatedPersonId, int relationalshipId)
        {
            var relatedPerson = new RelatedPerson
            {
                PersonId=personId,
                RelatedPersonId=relatedPersonId,
                RelationshipId=relationalshipId
            };


            _relatedPeople.Add(relatedPerson);
        }


        public void RemoveRelatedPerson(Guid personId, Guid relatedPersonId)
        {
            var relatedPerson = _relatedPeople.Find(x => x.PersonId==personId && x.RelatedPersonId==relatedPersonId);
            _relatedPeople.Remove(relatedPerson);
        }


        public void AddPhoto(string pictureUrl, string fileName, bool isMain = false)
        {
            var photo = new Photo
            {
                FileName = fileName,
                PictureUrl = pictureUrl
            };

            if (_photos.Count == 0) photo.IsMain = true;

            _photos.Add(photo);
        }


        public void RemovePhoto(int id)
        {
            var photo = _photos.Find(x => x.Id == id);
            _photos.Remove(photo);
        }

        public void SetMainPhoto(int id)
        {
            var currentMain = _photos.SingleOrDefault(item => item.IsMain);
            foreach (var item in _photos.Where(item => item.IsMain))
            {
                item.IsMain = false;
            }

            var photo = _photos.Find(x => x.Id == id);
            if (photo != null)
            {
                photo.IsMain = true;
                if (currentMain != null) currentMain.IsMain = false;
            }
        }

    }
}
