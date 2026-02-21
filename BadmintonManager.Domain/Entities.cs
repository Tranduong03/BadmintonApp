using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadmintonManager.Domain.Enums;

namespace BadmintonManager.Domain.Entities
{
    /// <summary>
    /// Đại diện cho một thành viên trong CLB. 
    /// Chứa thông tin cá nhân, vai trò và trình độ kỹ năng.
    /// </summary>
    public class Member
    {
        public Guid     Id { get; set; } = Guid.NewGuid();                        // Unique identifier for the member
        public string   Name { get; set; } = string.Empty;                        // Member's name
        public string?  PhoneNumber { get; set; }                                 // Optional phone number
        public Gender   Sex { get; set; } = Gender.Male;                          // Male / Female
        public Enums.MemberRole Role { get; set; } = Enums.MemberRole.Guest;      // Member's role with a default value

        public Guid? SkillLevelId { get; set; }         
        public Skill? SkillLevel { get; set; }

        // Navigation Properties (Quan hệ)
        public ICollection<SessionParticipant> SessionParticipants { get; set; } = new List<SessionParticipant>();
    }

    /// <summary>
    /// Đại diện cho một buổi sinh hoạt/chơi cầu lông (Session).
    /// Chứa thông tin tài chính tổng quan của buổi đó.
    /// </summary>
    public class Session
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? Note { get; set; }

        // Trạng thái buổi chơi
        public bool IsFinalized { get; set; } = false;      // False: Đang nhập liệu, True: Đã chốt sổ
        public decimal TotalActualCost { get; set; }        // Total cost incurred 
        public decimal AppliedCostPerPerson { get; set; }   // Mean cost per person 
        public decimal ClubFundContribution { get; set; }   // Profit 

        // Navigation Properties
        public ICollection<Cost> Costs { get; set; } = new List<Cost>();
        public ICollection<SessionParticipant> Participants { get; set; } = new List<SessionParticipant>();
    }

    /// <summary>
    /// Bảng trung gian (Many-to-Many) lưu thông tin tham gia của Member trong một Session.
    /// Quản lý việc ai đi đá, ai được miễn phí và trạng thái đóng tiền.
    /// </summary>
    public class SessionParticipant
    {
        public Guid     Id { get; set; } = Guid.NewGuid();
        public Guid     SessionId { get; set; }
        public Session  Session { get; set; } = null!;
        public Guid     MemberId { get; set; }
        public Member   Member { get; set; } = null!;
        public bool     IsExempt { get; set; } = false;
        public bool     IsPaid { get; set; } = false;   // Payment status
        public decimal  AmountDue { get; set; }         // Amount
    }

    /// <summary>
    /// Chi tiết các khoản chi phí phát sinh trong buổi chơi (VD: Mua nước, thuê sân, mua cầu).
    /// </summary>
    public class Cost
    {
        public Guid     Id { get; set; } = Guid.NewGuid();
        public string   Description { get; set; } = string.Empty; 
        public decimal  Amount { get; set; }
        public Guid     SessionId { get; set; }
        public Session  Session { get; set; } = null!;
    }

    /// <summary>
    /// Định nghĩa các cấp độ trình độ (Skill Level) để phục vụ thuật toán ghép trận cân bằng.
    /// </summary>
    public class Skill
    {
        public Guid     Id { get; set; } = Guid.NewGuid();    // Unique identifier for the skill
        public string   Name { get; set; } = string.Empty;    // Skill name: TB, TB+
        public string?  Description { get; set; }             // Optional description of the skill
        public int      ScoreWeight { get; set; }             // Weight of the skill in scoring
    }
}
